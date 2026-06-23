// ═══════════════════════════════════════════════════════
// ARCHIVO: CitaService.cs
// QUÉ HACE: El "empleado de recepción" que gestiona el agendamiento de citas.
//           Todas las operaciones (leer, crear, cambiar estado, cancelar)
//           se delegan a Stored Procedures porque estas acciones requieren
//           transacciones complejas (crear pago al crear cita, registrar auditoría, etc.).
// QUIÉN LO USA: CitaController (inyectado como ICitaService)
// ═══════════════════════════════════════════════════════

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
        var results = await _context.Set<CitaListSpResult>()
            .FromSqlRaw("EXEC sp_Cita_List @id_veterinario, @id_cliente, @id_mascota, @estado, @fecha_inicio, @fecha_fin",
                new SqlParameter("@id_veterinario", (object?)filter.IdVeterinario ?? DBNull.Value),
                new SqlParameter("@id_cliente", (object?)filter.IdCliente ?? DBNull.Value),
                new SqlParameter("@id_mascota", (object?)filter.IdMascota ?? DBNull.Value),
                new SqlParameter("@estado", (object?)filter.Estado ?? DBNull.Value),
                new SqlParameter("@fecha_inicio", (object?)filter.FechaInicio?.Date ?? DBNull.Value),
                new SqlParameter("@fecha_fin", (object?)filter.FechaFin?.Date ?? DBNull.Value))
            .ToListAsync();

        return results.Select(MapListResponse).ToList();
    }

    public async Task<List<CitaResponse>> GetTodayAsync(int? idVeterinario)
    {
        var results = await _context.Set<CitaListSpResult>()
            .FromSqlRaw("EXEC sp_Cita_ListToday @id_veterinario",
                new SqlParameter("@id_veterinario", (object?)idVeterinario ?? DBNull.Value))
            .ToListAsync();

        return results.Select(MapListResponse).ToList();
    }

    /// <summary>Devuelve las citas del cliente autenticado buscando su perfil por IdUsuario</summary>
    public async Task<List<CitaResponse>> GetMisCitasAsync(int idUsuario)
    {
        // El JWT tiene IdUsuario, no IdCliente — hay que resolver el perfil de cliente primero
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

    /// <summary>Crea la cita vía SP y captura el ID devuelto por SELECT</summary>
    public async Task<CitaResponse> CreateAsync(CreateCitaRequest request, int idUsuario)
    {
        // sp_Cita_Create devuelve SELECT @id_cita AS id_cita — no tiene OUTPUT ni @id_usuario
        // Usamos sintaxis @sp_param = @batch_param para evitar el mapeo posicional.
        // sp_Cita_Create tiene @duracion_minutos (INT, pos 4) y @estado (pos 6) opcionales en el medio,
        // por eso no podemos pasar positionally — SQL Server asignaría @motivo (nvarchar) a @duracion_minutos (INT).
        var results = await _context.Set<CitaCreateSpResult>()
            .FromSqlRaw(
                "EXEC sp_Cita_Create @id_mascota = @id_mascota, @id_veterinario = @id_veterinario, @fecha_hora = @fecha_hora, @motivo = @motivo, @observaciones = @observaciones",
                new SqlParameter("@id_mascota", request.IdMascota),
                new SqlParameter("@id_veterinario", request.IdVeterinario),
                new SqlParameter("@fecha_hora", request.FechaHora),
                new SqlParameter("@motivo", (object?)request.Motivo ?? DBNull.Value),
                new SqlParameter("@observaciones", (object?)request.Observaciones ?? DBNull.Value))
            .ToListAsync();

        var newId = results.First().id_cita;
        _logger.LogInformation("Cita creada: {Id}", newId);

        // Construimos el response directamente para evitar la segunda consulta a sp_Cita_Read
        // que puede fallar si el SP tiene joins que no devuelven fila (mismo patrón que sp_Cita_List).
        return new CitaResponse
        {
            IdCita = newId,
            IdMascota = request.IdMascota,
            IdVeterinario = request.IdVeterinario,
            FechaHora = request.FechaHora,
            Motivo = request.Motivo,
            Observaciones = request.Observaciones,
            Estado = "Pendiente",
        };
    }

    public async Task ChangeEstadoAsync(int id, ChangeEstadoCitaRequest request, int idUsuario)
    {
        // sp_Cita_ChangeStatus: @id_cita, @estado, @observaciones (opcional) — no tiene @id_usuario
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Cita_ChangeStatus @id_cita, @estado",
            new SqlParameter("@id_cita", id),
            new SqlParameter("@estado", request.Estado));
    }

    public async Task CancelAsync(int id, CancelCitaRequest request, int idUsuario)
    {
        // sp_Cita_Cancel: @id_cita, @motivo_cancelacion — no tiene @id_usuario
        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Cita_Cancel @id_cita, @motivo_cancelacion",
            new SqlParameter("@id_cita", id),
            new SqlParameter("@motivo_cancelacion", request.Motivo));
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
            NombreServicio = r.servicio_nombre ?? string.Empty,
            PrecioUnitario = r.precio_unitario,
            Subtotal = r.subtotal
        }).ToList();
    }

    private static CitaResponse MapResponse(CitaSpResult r) => new()
    {
        IdCita = r.id_cita,
        IdMascota = r.id_mascota,
        NombreMascota = r.mascota_nombre ?? string.Empty,
        Especie = r.mascota_especie ?? string.Empty,
        IdVeterinario = r.id_veterinario,
        NombreVeterinario = r.veterinario_nombre ?? string.Empty,
        IdCliente = r.id_cliente,
        NombreCliente = r.cliente_nombre ?? string.Empty,
        FechaHora = r.fecha_hora,
        Estado = r.estado ?? string.Empty,
        Motivo = r.motivo ?? string.Empty,
        Observaciones = r.observaciones,
    };

    private static CitaResponse MapListResponse(CitaListSpResult r) => new()
    {
        IdCita = r.id_cita,
        NombreMascota = r.mascota_nombre ?? string.Empty,
        Especie = r.mascota_especie ?? string.Empty,
        NombreVeterinario = r.veterinario_nombre ?? string.Empty,
        NombreCliente = r.cliente_nombre ?? string.Empty,
        FechaHora = r.fecha_hora,
        Estado = r.estado ?? string.Empty,
        Motivo = r.motivo ?? string.Empty,
    };
}
