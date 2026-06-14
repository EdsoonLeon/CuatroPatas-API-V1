// ═══════════════════════════════════════════════════════
// ARCHIVO: DashboardSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Dashboard_Resumen.
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: DashboardService.GetResumenAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ Las propiedades DEBEN ser snake_case para que EF las mapee correctamente desde FromSqlRaw
public class DashboardSpResult
{
    public int citas_completadas { get; set; }
    public int citas_pendientes { get; set; }
    public int citas_confirmadas { get; set; }
    public int citas_canceladas { get; set; }
    public int mascotas_atendidas { get; set; }
    public int medicamentos_bajo_stock { get; set; }
    public decimal ingresos_cobrados { get; set; }
}
