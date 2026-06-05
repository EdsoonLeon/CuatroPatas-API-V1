// ═══════════════════════════════════════════════════════
// ARCHIVO: DetalleCita.cs
// QUÉ HACE: Representa la tabla DETALLE_CITA de la base de datos.
//           Es la "línea de factura" de una cita — registra qué servicio se prestó
//           y a qué precio estaba EN ESE MOMENTO (no el precio actual del catálogo).
//           Así si el precio de un servicio cambia, las citas pasadas conservan su precio original.
// QUIÉN LO USA: CitaService al agregar servicios con sp_Cita_AddServicio
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("DETALLE_CITA")]
public class DetalleCita
{
    [Key]
    [Column("id_detalle")]
    public int IdDetalle { get; set; }

    [Column("id_cita")]
    public int IdCita { get; set; }

    [Column("id_servicio")]
    public int IdServicio { get; set; }

    // El precio del servicio en el momento en que se agregó a la cita.
    // El SP copia el precio actual del catálogo aquí para "congelarlo".
    [Column("precio_unitario")]
    public decimal PrecioUnitario { get; set; }

    // PrecioUnitario × cantidad (si hubiera más de 1, aunque actualmente es siempre 1)
    [Column("subtotal")]
    public decimal Subtotal { get; set; }

    public Cita Cita { get; set; } = null!;
    public Servicio Servicio { get; set; } = null!;
}
