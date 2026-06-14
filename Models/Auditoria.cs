using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("AUDITORIA")]
public class Auditoria
{
    [Key]
    [Column("id_auditoria")]
    public long IdAuditoria { get; set; }

    [Column("tabla_afectada")]
    public string TablaAfectada { get; set; } = string.Empty;

    [Column("operacion")]
    public string Operacion { get; set; } = string.Empty;

    [Column("id_registro")]
    public int IdRegistro { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("datos_anteriores")]
    public string? DatosAnteriores { get; set; }

    [Column("datos_nuevos")]
    public string? DatosNuevos { get; set; }

    [Column("fecha_evento")]
    public DateTime FechaEvento { get; set; } = DateTime.Now;

    [Column("ip_origen")]
    public string? IpOrigen { get; set; }
}
