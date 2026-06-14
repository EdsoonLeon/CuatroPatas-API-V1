// ═══════════════════════════════════════════════════════
// ARCHIVO: PrescripcionService.cs
// QUÉ HACE: El "dispensador de recetas" que gestiona las prescripciones médicas.
//           CreateAsync DEBE ir por SP porque la operación hace dos cosas a la vez:
//           registra la prescripción Y descuenta el stock del medicamento de forma atómica.
//           Si algo falla a mitad del proceso, la transacción del SP garantiza
//           que ningún cambio quede a medias (todo o nada).
//           Update y Delete van por EF directo porque son operaciones simples
//           que no afectan el stock.
// QUIÉN LO USA: PrescripcionController (inyectado como IPrescripcionService)
// ═══════════════════════════════════════════════════════

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.DTOs.Prescripcion;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Models.SpResults;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class PrescripcionService : IPrescripcionService
{
    private readonly AppDbContext _context;
    private readonly IPrescripcionRepository _repo;
    private readonly ILogger<PrescripcionService> _logger;

    public PrescripcionService(
        AppDbContext context,
        IPrescripcionRepository repo,
        ILogger<PrescripcionService> logger)
    {
        _context = context;
        _repo = repo;
        _logger = logger;
    }

    /// <summary>Crea la prescripción vía SP (que descuenta stock) y devuelve el registro por ID OUTPUT</summary>
    public async Task<PrescripcionResponse> CreateAsync(CreatePrescripcionRequest request)
    {
        // El SP descuenta el stock del medicamento internamente — no se hace en la app
        var idParam = new SqlParameter("@id_prescripcion", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Prescripcion_Create @id_historial, @id_medicamento, @dosis, @frecuencia, @duracion, @cantidad, @id_prescripcion OUTPUT",
            new SqlParameter("@id_historial", request.IdHistorial),
            new SqlParameter("@id_medicamento", request.IdMedicamento),
            new SqlParameter("@dosis", request.Dosis),
            new SqlParameter("@frecuencia", request.Frecuencia),
            new SqlParameter("@duracion", request.Duracion),
            new SqlParameter("@cantidad", request.Cantidad),
            idParam);

        var newId = (int)idParam.Value;
        _logger.LogInformation("Prescripción creada: {Id}", newId);

        var prescripcion = await _repo.GetByIdAsync(newId)
            ?? throw new NotFoundException($"Prescripción {newId} no encontrada.");

        return MapResponse(prescripcion);
    }

    public async Task<List<PrescripcionResponse>> GetByHistorialAsync(int idHistorial)
    {
        var results = await _context.Set<PrescripcionSpResult>()
            .FromSqlRaw("EXEC sp_Prescripcion_ListByHistorial @id_historial",
                new SqlParameter("@id_historial", idHistorial))
            .ToListAsync();

        return results.Select(MapSpResponse).ToList();
    }

    public async Task<List<PrescripcionResponse>> GetByMascotaAsync(int idMascota)
    {
        var results = await _context.Set<PrescripcionSpResult>()
            .FromSqlRaw("EXEC sp_Prescripcion_ListByMascota @id_mascota",
                new SqlParameter("@id_mascota", idMascota))
            .ToListAsync();

        return results.Select(MapSpResponse).ToList();
    }

    public async Task<PrescripcionResponse> UpdateAsync(int id, UpdatePrescripcionRequest request)
    {
        var prescripcion = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Prescripción {id} no encontrada.");

        prescripcion.Dosis = request.Dosis;
        prescripcion.Frecuencia = request.Frecuencia;
        prescripcion.DuracionDias = request.DuracionDias;
        prescripcion.Indicaciones = request.Indicaciones;
        prescripcion.FechaInicio = request.FechaInicio;
        prescripcion.FechaFin = request.FechaFin;

        var actualizada = await _repo.UpdateAsync(prescripcion);
        return MapResponse(actualizada);
    }

    public async Task DeleteAsync(int id)
    {
        _ = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Prescripción {id} no encontrada.");
        await _repo.DeleteAsync(id);
    }

    // Mapeo desde entidad EF (sin nombre del medicamento — se usa solo en Update/GetById directo)
    private static PrescripcionResponse MapResponse(Models.Prescripcion p) => new()
    {
        IdPrescripcion = p.IdPrescripcion,
        IdHistorial = p.IdHistorial,
        IdMedicamento = p.IdMedicamento,
        Dosis = p.Dosis,
        Frecuencia = p.Frecuencia,
        DuracionDias = p.DuracionDias,
        Indicaciones = p.Indicaciones,
        FechaInicio = p.FechaInicio,
        FechaFin = p.FechaFin
    };

    // Mapeo desde SpResult (incluye NombreMedicamento que el SP resuelve con JOIN)
    private static PrescripcionResponse MapSpResponse(PrescripcionSpResult r) => new()
    {
        IdPrescripcion = r.id_prescripcion,
        IdMedicamento = r.id_medicamento ?? 0,
        NombreMedicamento = r.medicamento_nombre ?? string.Empty,
        Dosis = r.dosis ?? string.Empty,
        Frecuencia = r.frecuencia,
        DuracionDias = r.duracion_dias,
        Indicaciones = r.indicaciones,
        FechaInicio = r.fecha_inicio,
        FechaFin = r.fecha_fin,
        NombreVeterinario = r.veterinario_nombre
    };
}
