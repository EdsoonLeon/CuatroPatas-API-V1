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

    [Column("precio_unitario")]
    public decimal PrecioUnitario { get; set; }

    [Column("subtotal")]
    public decimal Subtotal { get; set; }

    public Cita Cita { get; set; } = null!;
    public Servicio Servicio { get; set; } = null!;
}
