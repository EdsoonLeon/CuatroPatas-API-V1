// ═══════════════════════════════════════════════════════
// ARCHIVO: PagoResponse.cs
// QUÉ HACE: Datos de un pago para mostrar al personal de clínica.
//           Muestra el estado actual del cobro asociado a una cita.
// QUIÉN LO USA: PagoController en todas las respuestas de lectura
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Pago;

public class PagoResponse
{
    public int IdPago { get; set; }
    public int IdCita { get; set; }
    public decimal MontoTotal { get; set; }
    // "Pendiente", "Pagado" o "Anulado"
    public string EstadoPago { get; set; } = string.Empty;
    // "Efectivo", "Tarjeta", "Transferencia" — null si aún está pendiente
    public string? MetodoPago { get; set; }
    // La fecha en que se recibió el pago — null si aún está pendiente
    public DateTime? FechaPago { get; set; }
    public DateTime FechaCreacion { get; set; }
}
