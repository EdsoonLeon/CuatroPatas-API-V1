// ═══════════════════════════════════════════════════════
// ARCHIVO: UpdatePagoRequest.cs
// QUÉ HACE: Datos para registrar o actualizar el cobro de una cita.
//           Lo usa la recepción cuando el cliente paga: se marca el estado "Pagado",
//           se especifica cómo pagó y se registra la fecha/hora del pago.
// QUIÉN LO USA: PagoController.Update → PagoService.ActualizarPagoAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Pago;

public class UpdatePagoRequest
{
    // "Pendiente", "Pagado" o "Anulado"
    [Required]
    public string EstadoPago { get; set; } = string.Empty;

    // "Efectivo", "Tarjeta", "Transferencia", etc.
    public string? MetodoPago { get; set; }

    // Si es null, el SP usa la fecha/hora actual del servidor
    public DateTime? FechaPago { get; set; }
}
