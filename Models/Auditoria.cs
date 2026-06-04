using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("AUDITORIA")]
public class Auditoria
{
    [Key]
    [Column("id_auditoria")]
    public int IdAuditoria { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("accion")]
    public string Accion { get; set; } = string.Empty;

    [Column("tabla")]
    public string Tabla { get; set; } = string.Empty;

    [Column("id_registro")]
    public int? IdRegistro { get; set; }

    [Column("datos_anteriores")]
    public string? DatosAnteriores { get; set; }

    [Column("datos_nuevos")]
    public string? DatosNuevos { get; set; }

    [Column("fecha_accion")]
    public DateTime FechaAccion { get; set; } = DateTime.Now;

    [Column("ip_address")]
    public string? IpAddress { get; set; }
}
