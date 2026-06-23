// ═══════════════════════════════════════════════════════
// ARCHIVO: VeterinarioService.cs
// QUÉ HACE: El "empleado de RRHH" que gestiona los datos del personal veterinario.
//           CreateAsync es más complejo que en otros servicios: crea simultáneamente
//           la cuenta de Usuario (con contraseña hasheada) y el perfil de Veterinario,
//           y luego llama al SP para asignarle el rol "Veterinario" en un solo flujo.
//           GetAgendaAsync y GetEstadisticasAsync consultan SPs con JOINs para devolver
//           datos enriquecidos que el veterinario ve en su panel.
// QUIÉN LO USA: VeterinarioController (inyectado como IVeterinarioService)
// ═══════════════════════════════════════════════════════

using System.Data;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.DTOs.Veterinario;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Models;
using CuatroPatas.API.Models.SpResults;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class VeterinarioService : IVeterinarioService
{
    private readonly IVeterinarioRepository _repo;
    private readonly IUsuarioRepository _usuarioRepo;
    private readonly IRolRepository _rolRepo;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<VeterinarioService> _logger;

    public VeterinarioService(
        IVeterinarioRepository repo,
        IUsuarioRepository usuarioRepo,
        IRolRepository rolRepo,
        AppDbContext context,
        IMapper mapper,
        ILogger<VeterinarioService> logger)
    {
        _repo = repo;
        _usuarioRepo = usuarioRepo;
        _rolRepo = rolRepo;
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<VeterinarioResponse>> GetAllAsync()
    {
        var vets = await _repo.GetAllAsync();
        return _mapper.Map<List<VeterinarioResponse>>(vets);
    }

    public async Task<VeterinarioResponse> GetByIdAsync(int id)
    {
        var vet = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Veterinario {id} no encontrado.");
        return _mapper.Map<VeterinarioResponse>(vet);
    }

    /// <summary>Crea el perfil de veterinario junto con su cuenta de usuario y rol "Veterinario"</summary>
    public async Task<VeterinarioResponse> CreateAsync(CreateVeterinarioRequest request)
    {
        var existe = await _repo.GetByEmailAsync(request.Email);
        if (existe != null)
            throw new ConflictException("Ya existe un veterinario con ese email.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);
        var usuario = new Usuario
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            Activo = true,
            FechaRegistro = DateTime.Now
        };
        await _usuarioRepo.CreateAsync(usuario);

        var rolVeterinario = await _rolRepo.GetByNameAsync("Veterinario")
            ?? throw new InvalidOperationException("El rol 'Veterinario' no existe en el sistema.");
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Usuario_AssignRole @id_usuario, @id_rol",
            new SqlParameter("@id_usuario", usuario.IdUsuario),
            new SqlParameter("@id_rol", rolVeterinario.IdRol));

        var vet = _mapper.Map<Veterinario>(request);
        vet.IdUsuario = usuario.IdUsuario;
        vet.FechaAlta = DateTime.Now;

        var creado = await _repo.CreateAsync(vet);
        _logger.LogInformation("Veterinario creado: {Id}", creado.IdVeterinario);
        return _mapper.Map<VeterinarioResponse>(creado);
    }

    public async Task<VeterinarioResponse> UpdateAsync(int id, UpdateVeterinarioRequest request)
    {
        var vet = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Veterinario {id} no encontrado.");
        _mapper.Map(request, vet);
        var actualizado = await _repo.UpdateAsync(vet);
        return _mapper.Map<VeterinarioResponse>(actualizado);
    }

    public async Task SoftDeleteAsync(int id)
    {
        _ = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Veterinario {id} no encontrado.");
        await _repo.SoftDeleteAsync(id);
    }

    public async Task<List<AgendaResponse>> GetAgendaAsync(int idVeterinario, DateTime fechaInicio, DateTime fechaFin)
    {
        var results = await _context.Set<AgendaSpResult>()
            .FromSqlRaw("EXEC sp_Veterinario_GetAgenda @id_veterinario, @fecha_inicio, @fecha_fin",
                new SqlParameter("@id_veterinario", idVeterinario),
                new SqlParameter("@fecha_inicio", fechaInicio.Date),
                new SqlParameter("@fecha_fin", fechaFin.Date))
            .ToListAsync();

        return results.Select(r => new AgendaResponse
        {
            IdCita = r.id_cita,
            FechaHora = r.fecha_hora,
            Estado = r.estado ?? string.Empty,
            Motivo = r.motivo ?? string.Empty,
            NombreMascota = r.mascota_nombre ?? string.Empty,
            Especie = r.especie ?? string.Empty,
            NombreCliente = r.cliente_nombre ?? string.Empty,
            TelefonoCliente = r.cliente_telefono
        }).ToList();
    }

    public async Task<EstadisticasResponse> GetEstadisticasAsync(int idVeterinario, DateTime? fechaInicio, DateTime? fechaFin)
    {
        var results = await _context.Set<EstadisticasSpResult>()
            .FromSqlRaw("EXEC sp_Veterinario_GetStats @id_veterinario, @fecha_inicio, @fecha_fin",
                new SqlParameter("@id_veterinario", idVeterinario),
                new SqlParameter("@fecha_inicio", (object?)fechaInicio?.Date ?? DBNull.Value),
                new SqlParameter("@fecha_fin", (object?)fechaFin?.Date ?? DBNull.Value))
            .ToListAsync();

        var r = results.FirstOrDefault() ?? new EstadisticasSpResult();
        return new EstadisticasResponse
        {
            CitasCompletadas = r.citas_completadas,
            CitasCanceladas = r.citas_canceladas,
            CitasPendientes = r.citas_pendientes,
            CitasConfirmadas = r.citas_confirmadas,
            MascotasAtendidas = r.mascotas_atendidas
        };
    }
}
