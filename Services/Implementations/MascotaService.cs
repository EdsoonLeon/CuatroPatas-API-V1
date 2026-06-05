// ═══════════════════════════════════════════════════════
// ARCHIVO: MascotaService.cs
// QUÉ HACE: El "empleado de archivo clínico" que gestiona los expedientes de las mascotas.
//           El CRUD básico usa EF Core directamente (más simple y eficiente para estas operaciones).
//           GetHistorialAsync y GetProximasCitasAsync usan SPs porque necesitan JOINs con múltiples
//           tablas para devolver datos "desnormalizados" listos para mostrar en pantalla,
//           sin que el frontend tenga que hacer múltiples peticiones para armarlos.
// QUIÉN LO USA: MascotaController (inyectado como IMascotaService)
// ═══════════════════════════════════════════════════════

using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.DTOs.Cita;
using CuatroPatas.API.DTOs.Historial;
using CuatroPatas.API.DTOs.Mascota;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Models;
using CuatroPatas.API.Models.SpResults;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class MascotaService : IMascotaService
{
    private readonly IMascotaRepository _repo;
    private readonly IClienteRepository _clienteRepo;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<MascotaService> _logger;

    public MascotaService(
        IMascotaRepository repo,
        IClienteRepository clienteRepo,
        AppDbContext context,
        IMapper mapper,
        ILogger<MascotaService> logger)
    {
        _repo = repo;
        _clienteRepo = clienteRepo;
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<MascotaResponse>> GetAllAsync()
    {
        var mascotas = await _repo.GetAllAsync();
        return _mapper.Map<List<MascotaResponse>>(mascotas);
    }

    public async Task<MascotaResponse> GetByIdAsync(int id)
    {
        var mascota = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Mascota {id} no encontrada.");
        return _mapper.Map<MascotaResponse>(mascota);
    }

    public async Task<MascotaResponse> CreateAsync(CreateMascotaRequest request)
    {
        _ = await _clienteRepo.GetByIdAsync(request.IdCliente)
            ?? throw new NotFoundException($"Cliente {request.IdCliente} no encontrado.");

        var mascota = _mapper.Map<Mascota>(request);
        var creada = await _repo.CreateAsync(mascota);
        _logger.LogInformation("Mascota creada: {Id}", creada.IdMascota);
        return _mapper.Map<MascotaResponse>(creada);
    }

    public async Task<MascotaResponse> UpdateAsync(int id, UpdateMascotaRequest request)
    {
        var mascota = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Mascota {id} no encontrada.");
        _mapper.Map(request, mascota);
        var actualizada = await _repo.UpdateAsync(mascota);
        return _mapper.Map<MascotaResponse>(actualizada);
    }

    public async Task SoftDeleteAsync(int id)
    {
        _ = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Mascota {id} no encontrada.");
        await _repo.SoftDeleteAsync(id);
    }

    public async Task<List<HistorialResponse>> GetHistorialAsync(int idMascota)
    {
        var results = await _context.Set<HistorialSpResult>()
            .FromSqlRaw("EXEC sp_Mascota_GetHistorial @id_mascota",
                new SqlParameter("@id_mascota", idMascota))
            .ToListAsync();

        return results.Select(r => new HistorialResponse
        {
            IdHistorial = r.id_historial,
            IdMascota = r.id_mascota,
            NombreMascota = r.nombre_mascota ?? string.Empty,
            IdVeterinario = r.id_veterinario,
            NombreVeterinario = r.nombre_veterinario ?? string.Empty,
            ApellidoVeterinario = r.apellido_veterinario ?? string.Empty,
            IdCita = r.id_cita,
            TipoRegistro = r.tipo_registro ?? string.Empty,
            Descripcion = r.descripcion ?? string.Empty,
            Diagnostico = r.diagnostico,
            Tratamiento = r.tratamiento,
            FechaRegistro = r.fecha_registro
        }).ToList();
    }

    public async Task<List<CitaResponse>> GetProximasCitasAsync(int idMascota)
    {
        var results = await _context.Set<CitaSpResult>()
            .FromSqlRaw("EXEC sp_Mascota_GetProximasCitas @id_mascota",
                new SqlParameter("@id_mascota", idMascota))
            .ToListAsync();

        return results.Select(MapCitaResponse).ToList();
    }

    private static CitaResponse MapCitaResponse(CitaSpResult r) => new()
    {
        IdCita = r.id_cita,
        IdMascota = r.id_mascota,
        NombreMascota = r.nombre_mascota ?? string.Empty,
        Especie = r.especie ?? string.Empty,
        IdVeterinario = r.id_veterinario,
        NombreVeterinario = r.nombre_veterinario ?? string.Empty,
        ApellidoVeterinario = r.apellido_veterinario ?? string.Empty,
        IdCliente = r.id_cliente,
        NombreCliente = r.nombre_cliente ?? string.Empty,
        ApellidoCliente = r.apellido_cliente ?? string.Empty,
        FechaHora = r.fecha_hora,
        Estado = r.estado ?? string.Empty,
        Motivo = r.motivo ?? string.Empty,
        Observaciones = r.observaciones,
        FechaCreacion = r.fecha_creacion
    };
}
