// ═══════════════════════════════════════════════════════
// ARCHIVO: DashboardResponse.cs
// QUÉ HACE: Datos del panel de control principal para el administrador.
//           Agrupa en un solo objeto todas las métricas clave de la clínica:
//           citas, clientes, ingresos y alertas de inventario.
//           Se obtiene en una sola llamada a sp_Dashboard_Resumen.
// QUIÉN LO USA: DashboardController.GetResumen
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Dashboard;

public class DashboardResponse
{
    public int TotalCitasHoy { get; set; }
    public int TotalCitasSemana { get; set; }
    public int TotalCitasMes { get; set; }
    public int TotalClientes { get; set; }
    public int TotalMascotas { get; set; }
    public int TotalVeterinarios { get; set; }
    // Citas esperando ser atendidas hoy
    public int CitasPendientes { get; set; }
    // Citas en atención en este momento
    public int CitasEnCurso { get; set; }
    public int CitasCompletadasHoy { get; set; }
    public decimal IngresosHoy { get; set; }
    public decimal IngresosMes { get; set; }
    // Cantidad de medicamentos que están por debajo de su stock mínimo — alerta de inventario
    public int MedicamentosStockBajo { get; set; }
}
