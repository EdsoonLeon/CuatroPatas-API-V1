// ═══════════════════════════════════════════════════════
// ARCHIVO: DashboardService.cs
// QUÉ HACE: El "contador general" que prepara el resumen ejecutivo de la clínica.
//           Una sola llamada al SP sp_Dashboard_Resumen devuelve todas las métricas
//           del panel de control: citas del día, semana y mes, totales de clientes
//           y mascotas, ingresos y alertas de inventario.
//           Usar un solo SP en vez de múltiples consultas individuales reduce
//           significativamente el número de viajes a la base de datos.
// QUIÉN LO USA: DashboardController (inyectado como IDashboardService)
// ═══════════════════════════════════════════════════════

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.DTOs.Dashboard;
using CuatroPatas.API.Models.SpResults;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly AppDbContext _context;
    private readonly ILogger<DashboardService> _logger;

    public DashboardService(AppDbContext context, ILogger<DashboardService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DashboardResponse> GetResumenAsync(DateTime? fechaInicio, DateTime? fechaFin)
    {
        var results = await _context.Set<DashboardSpResult>()
            .FromSqlRaw("EXEC sp_Dashboard_Resumen @fecha_inicio, @fecha_fin",
                new SqlParameter("@fecha_inicio", (object?)fechaInicio?.Date ?? DBNull.Value),
                new SqlParameter("@fecha_fin", (object?)fechaFin?.Date ?? DBNull.Value))
            .ToListAsync();

        var r = results.FirstOrDefault() ?? new DashboardSpResult();

        _logger.LogInformation("Dashboard consultado con fechas {FechaInicio} - {FechaFin}", fechaInicio, fechaFin);

        return new DashboardResponse
        {
            TotalCitasHoy = r.total_citas_hoy,
            TotalCitasSemana = r.total_citas_semana,
            TotalCitasMes = r.total_citas_mes,
            TotalClientes = r.total_clientes,
            TotalMascotas = r.total_mascotas,
            TotalVeterinarios = r.total_veterinarios,
            CitasPendientes = r.citas_pendientes,
            CitasEnCurso = r.citas_en_curso,
            CitasCompletadasHoy = r.citas_completadas_hoy,
            IngresosHoy = r.ingresos_hoy,
            IngresosMes = r.ingresos_mes,
            MedicamentosStockBajo = r.medicamentos_stock_bajo
        };
    }
}
