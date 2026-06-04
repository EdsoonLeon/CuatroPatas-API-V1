using System.Data;
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

    public async Task<HistorialResponse> CreateAsync(CreateHistorialRequest request)
    {
        var idParam = new SqlParameter("@id_historial", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_HistorialMedico_Create @id_mascota, @id_veterinario, @id_cita, @tipo_registro, @descripcion, @diagnostico, @tratamiento, @id_historial OUTPUT",
            new SqlParameter("@id_mascota", request.IdMascota),
            new SqlParameter("@id_veterinario", request.IdVeterinario),
            new SqlParameter("@id_cita", (object?)request.IdCita ?? DBNull.Value),
            new SqlParameter("@tipo_registro", request.TipoRegistro),
            new SqlParameter("@descripcion", request.Descripcion),
            new SqlParameter("@diagnostico", (object?)request.Diagnostico ?? DBNull.Value),
            new SqlParameter("@tratamiento", (object?)request.Tratamiento ?? DBNull.Value),
            idParam);

        var newId = (int)idParam.Value;
        _logger.LogInformation("Historial médico creado: {Id}", newId);
        return await GetByIdAsync(newId);
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
            TipoRegistro = historial.TipoRegistro,
            Descripcion = historial.Descripcion,
            Diagnostico = historial.Diagnostico,
            Tratamiento = historial.Tratamiento,
            FechaRegistro = historial.FechaRegistro
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
    };
}
