// ═══════════════════════════════════════════════════════
// ARCHIVO: EstadisticasResponse.cs
// QUÉ HACE: Métricas de rendimiento de un veterinario en un período dado.
//           Muestra cuántas citas tuvo, cuántas completó, cuántas se cancelaron
//           y cuánto generó en ingresos — útil para reportes internos y comisiones.
// QUIÉN LO USA: VeterinarioController.GetEstadisticas
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Veterinario;

public class EstadisticasResponse
{
    public int TotalCitas { get; set; }
    public int CitasCompletadas { get; set; }
    public int CitasCanceladas { get; set; }
    public int CitasPendientes { get; set; }
    // Suma de montos de pagos en estado "Pagado" durante el período consultado
    public decimal IngresosTotal { get; set; }
    // Descripción textual del período: "Enero 2025", "2025", etc.
    public string? Periodo { get; set; }
}
