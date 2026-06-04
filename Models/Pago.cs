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

    [Column("monto_total")]
    public decimal MontoTotal { get; set; }

    [Column("estado_pago")]
    public string EstadoPago { get; set; } = "Pendiente";

    [Column("metodo_pago")]
    public string? MetodoPago { get; set; }

    [Column("fecha_pago")]
    public DateTime? FechaPago { get; set; }

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public Cita Cita { get; set; } = null!;
}
