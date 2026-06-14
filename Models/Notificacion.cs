using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("NOTIFICACION")]
public class Notificacion
{
    [Key]
    [Column("id_notificacion")]
    public int IdNotificacion { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_cita")]
    public int? IdCita { get; set; }

    [Column("tipo")]
    public string Tipo { get; set; } = string.Empty;

    [Column("canal")]
    public string Canal { get; set; } = string.Empty;

    [Column("mensaje")]
    public string Mensaje { get; set; } = string.Empty;

    [Column("enviado")]
    public bool Enviado { get; set; } = false;

    [Column("fecha_programada")]
    public DateTime FechaProgramada { get; set; } = DateTime.Now;

    [Column("fecha_enviado")]
    public DateTime? FechaEnviado { get; set; }

    public Usuario Usuario { get; set; } = null!;
}
