// ═══════════════════════════════════════════════════════
// ARCHIVO: IDashboardService.cs
// QUÉ HACE: Contrato del servicio del panel de control.
//           Interfaz de un solo método porque todo el dashboard sale
//           de una única llamada al SP sp_Dashboard_Resumen que agrega
//           todos los datos de la clínica en una sola consulta.
// QUIÉN LO USA: DashboardController (inyectado como IDashboardService)
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.DTOs.Dashboard;

namespace CuatroPatas.API.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardResponse> GetResumenAsync(DateTime? fechaInicio, DateTime? fechaFin);
}
