// ═══════════════════════════════════════════════════════
// ARCHIVO: DashboardSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Dashboard_Resumen.
//           Es el "cuadro de mando" que el admin ve al entrar al sistema:
//           citas del día, semana y mes, totales de clientes y mascotas,
//           ingresos y alertas de medicamentos con stock bajo.
//           El SP calcula todo esto en una sola consulta — sin cargar la app con múltiples requests.
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: DashboardRepository.ObtenerResumenAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ Las propiedades DEBEN ser snake_case para que EF las mapee correctamente desde FromSqlRaw
public class DashboardSpResult
{
    // Citas del día de hoy en cualquier estado
    public int total_citas_hoy { get; set; }
    public int total_citas_semana { get; set; }
    public int total_citas_mes { get; set; }
    // Totales de registros activos en la clínica
    public int total_clientes { get; set; }
    public int total_mascotas { get; set; }
    public int total_veterinarios { get; set; }
    // Citas en estado "Pendiente" hoy — hay pacientes esperando ser atendidos
    public int citas_pendientes { get; set; }
    // Citas en estado "En Curso" — están siendo atendidas ahora mismo
    public int citas_en_curso { get; set; }
    public int citas_completadas_hoy { get; set; }
    public decimal ingresos_hoy { get; set; }
    public decimal ingresos_mes { get; set; }
    // Cuántos medicamentos están por debajo de su stock mínimo — alerta de inventario
    public int medicamentos_stock_bajo { get; set; }
}
