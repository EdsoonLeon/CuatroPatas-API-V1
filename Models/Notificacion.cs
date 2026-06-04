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

    [Column("titulo")]
    public string Titulo { get; set; } = string.Empty;

    [Column("mensaje")]
    public string Mensaje { get; set; } = string.Empty;

    [Column("tipo")]
    public string Tipo { get; set; } = string.Empty;

    [Column("leida")]
    public bool Leida { get; set; } = false;

    [Column("enviada")]
    public bool Enviada { get; set; } = false;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("fecha_envio")]
    public DateTime? FechaEnvio { get; set; }

    public Usuario Usuario { get; set; } = null!;
}
