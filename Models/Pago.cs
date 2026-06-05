// ═══════════════════════════════════════════════════════
// ARCHIVO: Pago.cs
// QUÉ HACE: Representa la tabla PAGO de la base de datos.
//           Cada cita tiene exactamente un pago asociado (relación 1:1).
//           El Stored Procedure sp_Cita_Create crea el pago automáticamente
//           cuando se crea la cita. El personal de clínica lo actualiza después
//           cuando el cliente cancela la deuda.
// QUIÉN LO USA: PagoService, PagoRepository
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("PAGO")]
public class Pago
{
    [Key]
    [Column("id_pago")]
    public int IdPago { get; set; }

    [Column("id_cita")]
    public int IdCita { get; set; }

    // El total a pagar — lo calcula el SP sumando los subtotales de DetalleCita
    [Column("monto_total")]
    public decimal MontoTotal { get; set; }

    // Estado del pago: "Pendiente", "Pagado", "Anulado"
    [Column("estado_pago")]
    public string EstadoPago { get; set; } = "Pendiente";

    // Cómo pagó el cliente: "Efectivo", "Tarjeta", "Transferencia", etc.
    // Es null hasta que el pago se registra
    [Column("metodo_pago")]
    public string? MetodoPago { get; set; }

    // La fecha y hora exacta en que se recibió el pago (null si aún está pendiente)
    [Column("fecha_pago")]
    public DateTime? FechaPago { get; set; }

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public Cita Cita { get; set; } = null!;
}
