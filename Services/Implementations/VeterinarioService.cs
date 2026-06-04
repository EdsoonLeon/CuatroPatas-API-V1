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
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<VeterinarioService> _logger;

    public VeterinarioService(
        IVeterinarioRepository repo,
        IUsuarioRepository usuarioRepo,
        AppDbContext context,
        IMapper mapper,
        ILogger<VeterinarioService> logger)
    {
        _repo = repo;
        _usuarioRepo = usuarioRepo;
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

    public async Task<VeterinarioResponse> CreateAsync(CreateVeterinarioRequest request)
    {
        var existe = await _repo.GetByEmailAsync(request.Email);
        if (existe != null)
            throw new ConflictException("Ya existe un veterinario con ese email.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);
        var usuario = new Usuario
        {
            NombreUsuario = $"{request.Nombre} {request.Apellido}",
            Email = request.Email,
            PasswordHash = passwordHash,
            Activo = true,
            FechaCreacion = DateTime.Now
        };
        await _usuarioRepo.CreateAsync(usuario);

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Usuario_AssignRole @id_usuario, @nombre_rol",
            new SqlParameter("@id_usuario", usuario.IdUsuario),
            new SqlParameter("@nombre_rol", "Veterinario"));

        var vet = _mapper.Map<Veterinario>(request);
        vet.IdUsuario = usuario.IdUsuario;
        vet.FechaRegistro = DateTime.Now;

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
            NombreMascota = r.nombre_mascota ?? string.Empty,
            Especie = r.especie ?? string.Empty,
            NombreCliente = r.nombre_cliente ?? string.Empty,
            ApellidoCliente = r.apellido_cliente ?? string.Empty,
            TelefonoCliente = r.telefono_cliente
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
            TotalCitas = r.total_citas,
            CitasCompletadas = r.citas_completadas,
            CitasCanceladas = r.citas_canceladas,
            CitasPendientes = r.citas_pendientes,
            IngresosTotal = r.ingresos_total,
            Periodo = r.periodo
        };
    }
}
