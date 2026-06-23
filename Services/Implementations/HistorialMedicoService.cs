// ═══════════════════════════════════════════════════════
// ARCHIVO: HistorialMedicoService.cs
// QUÉ HACE: El "archivista clínico" que guarda y consulta el expediente médico de las mascotas.
//           CreateAsync usa SP porque la creación incluye auditoría automática en la BD.
//           Los listados también usan SPs para traer datos enriquecidos (nombre del vet, mascota)
//           sin necesitar consultas adicionales.
//           GetByIdAsync y SoftDeleteAsync van por EF directo o SP liviano porque
//           no necesitan datos de otras tablas.
// QUIÉN LO USA: HistorialController (inyectado como IHistorialMedicoService)
// ═══════════════════════════════════════════════════════

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.DTOs.Historial;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Models.SpResults;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class HistorialMedicoService : IHistorialMedicoService
{
    private readonly AppDbContext _context;
    private readonly IHistorialMedicoRepository _repo;
    private readonly ILogger<HistorialMedicoService> _logger;

    public HistorialMedicoService(
        AppDbContext context,
        IHistorialMedicoRepository repo,
        ILogger<HistorialMedicoService> logger)
    {
        _context = context;
        _repo = repo;
        _logger = logger;
    }

    /// <summary>Crea el historial vía SP y construye el response desde los datos del request</summary>
    public async Task<HistorialResponse> CreateAsync(CreateHistorialRequest request)
    {
        // Usamos @sp_param = @batch_param porque @fecha (pos 4) es opcional y está en el medio.
        // Sin named params, @tipo (nvarchar) se mapearía posicionalmente a @fecha (DATE) → error de conversión.
        var results = await _context.Set<HistorialCreateSpResult>()
            .FromSqlRaw(
                "EXEC sp_HistorialMedico_Create @id_mascota = @id_mascota, @id_cita = @id_cita, @id_veterinario = @id_veterinario, @tipo = @tipo, @diagnostico = @diagnostico, @tratamiento = @tratamiento, @observaciones = @observaciones",
                new SqlParameter("@id_mascota", request.IdMascota),
                new SqlParameter("@id_cita", (object?)request.IdCita ?? DBNull.Value),
                new SqlParameter("@id_veterinario", request.IdVeterinario),
                new SqlParameter("@tipo", request.TipoRegistro),
                new SqlParameter("@diagnostico", (object?)request.Diagnostico ?? DBNull.Value),
                new SqlParameter("@tratamiento", (object?)request.Tratamiento ?? DBNull.Value),
                new SqlParameter("@observaciones", (object?)request.Descripcion ?? DBNull.Value))
            .ToListAsync();

        var newId = (int)results.First().id_historial;
        _logger.LogInformation("Historial médico creado: {Id}", newId);

        // Construimos el response directamente desde el request para evitar una segunda
        // consulta EF Core que puede fallar si el contexto tiene problemas de mapeo.
        return new HistorialResponse
        {
            IdHistorial = newId,
            IdMascota = request.IdMascota,
            IdVeterinario = request.IdVeterinario,
            IdCita = request.IdCita,
            Fecha = DateOnly.FromDateTime(DateTime.Today),
            Tipo = request.TipoRegistro,
            Diagnostico = request.Diagnostico,
            Tratamiento = request.Tratamiento,
            Observaciones = request.Descripcion,
        };
    }

    public async Task<HistorialResponse> GetByIdAsync(int id)
    {
        var historial = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Historial {id} no encontrado.");

        return new HistorialResponse
        {
            IdHistorial = historial.IdHistorial,
            IdMascota = historial.IdMascota,
            IdVeterinario = historial.IdVeterinario,
            IdCita = historial.IdCita,
            Fecha = historial.Fecha,
            Tipo = historial.Tipo,
            Diagnostico = historial.Diagnostico,
            Tratamiento = historial.Tratamiento,
            Observaciones = historial.Observaciones,
            Peso = historial.Peso,
            Temperatura = historial.Temperatura
        };
    }

    public async Task<List<HistorialResponse>> GetByMascotaAsync(int idMascota, string? tipoRegistro, DateTime? fechaInicio, DateTime? fechaFin)
    {
        var results = await _context.Set<HistorialSpResult>()
            .FromSqlRaw("EXEC sp_HistorialMedico_ListByMascota @id_mascota, @tipo_registro, @fecha_inicio, @fecha_fin",
                new SqlParameter("@id_mascota", idMascota),
                new SqlParameter("@tipo_registro", (object?)tipoRegistro ?? DBNull.Value),
                new SqlParameter("@fecha_inicio", (object?)fechaInicio?.Date ?? DBNull.Value),
                new SqlParameter("@fecha_fin", (object?)fechaFin?.Date ?? DBNull.Value))
            .ToListAsync();

        return results.Select(MapResponse).ToList();
    }

    public async Task<List<HistorialResponse>> GetVacunasAsync(int idMascota)
    {
        var results = await _context.Set<HistorialSpResult>()
            .FromSqlRaw("EXEC sp_HistorialMedico_ListVacunas @id_mascota",
                new SqlParameter("@id_mascota", idMascota))
            .ToListAsync();

        return results.Select(MapResponse).ToList();
    }

    public async Task SoftDeleteAsync(int id)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_HistorialMedico_Delete @id_historial",
            new SqlParameter("@id_historial", id));
    }

    private static HistorialResponse MapResponse(HistorialSpResult r) => new()
    {
        IdHistorial = r.id_historial,
        Fecha = r.fecha,
        Tipo = r.tipo ?? string.Empty,
        Diagnostico = r.diagnostico,
        Tratamiento = r.tratamiento,
        Peso = r.peso,
        Temperatura = r.temperatura,
        NombreVeterinario = r.veterinario_nombre ?? string.Empty
    };
}
