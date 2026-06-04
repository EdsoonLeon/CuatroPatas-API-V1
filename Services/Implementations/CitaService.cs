using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.DTOs.Cita;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Models.SpResults;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class CitaService : ICitaService
{
    private readonly AppDbContext _context;
    private readonly IClienteRepository _clienteRepo;
    private readonly ILogger<CitaService> _logger;

    public CitaService(AppDbContext context, IClienteRepository clienteRepo, ILogger<CitaService> logger)
    {
        _context = context;
        _clienteRepo = clienteRepo;
        _logger = logger;
    }

    public async Task<List<CitaResponse>> GetAllAsync(CitaFilterRequest filter)
    {
        var results = await _context.Set<CitaSpResult>()
            .FromSqlRaw("EXEC sp_Cita_List @id_veterinario, @id_cliente, @id_mascota, @estado, @fecha_inicio, @fecha_fin",
                new SqlParameter("@id_veterinario", (object?)filter.IdVeterinario ?? DBNull.Value),
                new SqlParameter("@id_cliente", (object?)filter.IdCliente ?? DBNull.Value),
                new SqlParameter("@id_mascota", (object?)filter.IdMascota ?? DBNull.Value),
                new SqlParameter("@estado", (object?)filter.Estado ?? DBNull.Value),
                new SqlParameter("@fecha_inicio", (object?)filter.FechaInicio?.Date ?? DBNull.Value),
                new SqlParameter("@fecha_fin", (object?)filter.FechaFin?.Date ?? DBNull.Value))
            .ToListAsync();

        return results.Select(MapResponse).ToList();
    }

    public async Task<List<CitaResponse>> GetTodayAsync(int? idVeterinario)
    {
        var results = await _context.Set<CitaSpResult>()
            .FromSqlRaw("EXEC sp_Cita_ListToday @id_veterinario",
                new SqlParameter("@id_veterinario", (object?)idVeterinario ?? DBNull.Value))
            .ToListAsync();

        return results.Select(MapResponse).ToList();
    }

    public async Task<List<CitaResponse>> GetMisCitasAsync(int idUsuario)
    {
        var cliente = await _clienteRepo.GetByUsuarioIdAsync(idUsuario)
            ?? throw new NotFoundException("No se encontró perfil de cliente para este usuario.");

        return await GetAllAsync(new CitaFilterRequest { IdCliente = cliente.IdCliente });
    }

    public async Task<CitaResponse> GetByIdAsync(int id)
    {
        var results = await _context.Set<CitaSpResult>()
            .FromSqlRaw("EXEC sp_Cita_Read @id_cita",
                new SqlParameter("@id_cita", id))
            .ToListAsync();

        var cita = results.FirstOrDefault()
            ?? throw new NotFoundException($"Cita {id} no encontrada.");

        return MapResponse(cita);
    }

    public async Task<CitaResponse> CreateAsync(CreateCitaRequest request, int idUsuario)
    {
        var idCitaParam = new SqlParameter("@id_cita", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Cita_Create @id_mascota, @id_veterinario, @fecha_hora, @motivo, @observaciones, @id_usuario, @id_cita OUTPUT",
            new SqlParameter("@id_mascota", request.IdMascota),
            new SqlParameter("@id_veterinario", request.IdVeterinario),
            new SqlParameter("@fecha_hora", request.FechaHora),
            new SqlParameter("@motivo", request.Motivo),
            new SqlParameter("@observaciones", (object?)request.Observaciones ?? DBNull.Value),
            new SqlParameter("@id_usuario", idUsuario),
            idCitaParam);

        var newId = (int)idCitaParam.Value;
        _logger.LogInformation("Cita creada: {Id}", newId);
        return await GetByIdAsync(newId);
    }

    public async Task ChangeEstadoAsync(int id, ChangeEstadoCitaRequest request, int idUsuario)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Cita_ChangeStatus @id_cita, @estado, @id_usuario",
            new SqlParameter("@id_cita", id),
            new SqlParameter("@estado", request.Estado),
            new SqlParameter("@id_usuario", idUsuario));
    }

    public async Task CancelAsync(int id, CancelCitaRequest request, int idUsuario)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Cita_Cancel @id_cita, @motivo, @id_usuario",
            new SqlParameter("@id_cita", id),
            new SqlParameter("@motivo", request.Motivo),
            new SqlParameter("@id_usuario", idUsuario));
    }

    public async Task AddServicioAsync(int id, AddServicioCitaRequest request)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Cita_AddServicio @id_cita, @id_servicio",
            new SqlParameter("@id_cita", id),
            new SqlParameter("@id_servicio", request.IdServicio));
    }

    public async Task<List<CitaServicioResponse>> GetServiciosAsync(int id)
    {
        var results = await _context.Set<CitaServicioSpResult>()
            .FromSqlRaw("EXEC sp_Cita_GetServicios @id_cita",
                new SqlParameter("@id_cita", id))
            .ToListAsync();

        return results.Select(r => new CitaServicioResponse
        {
            IdServicio = r.id_servicio,
            NombreServicio = r.nombre_servicio ?? string.Empty,
            PrecioUnitario = r.precio_unitario,
            Subtotal = r.subtotal
        }).ToList();
    }

    private static CitaResponse MapResponse(CitaSpResult r) => new()
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
