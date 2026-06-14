// ═══════════════════════════════════════════════════════
// ARCHIVO: DashboardResponse.cs
// QUÉ HACE: Datos del panel de control principal para el administrador.
//           Se obtiene en una sola llamada a sp_Dashboard_Resumen.
// QUIÉN LO USA: DashboardController.GetResumen
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Dashboard;

public class DashboardResponse
{
    public int TotalCitas { get; set; }
    public int CitasCompletadas { get; set; }
    public int CitasPendientes { get; set; }
    public int CitasConfirmadas { get; set; }
    public int CitasCanceladas { get; set; }
    public int MascotasAtendidas { get; set; }
    public int MedicamentosBajoStock { get; set; }
    public decimal IngresosCobrados { get; set; }
}
