using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("USUARIO")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [Column("ultimo_acceso")]
    public DateTime? UltimoAcceso { get; set; }

    [Column("intentos_fallidos")]
    public int IntentosFallidos { get; set; } = 0;

    [Column("bloqueado")]
    public bool Bloqueado { get; set; } = false;

    [Column("fecha_bloqueo")]
    public DateTime? FechaBloqueo { get; set; }

    public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
}
