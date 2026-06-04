using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("USUARIO_ROL")]
public class UsuarioRol
{
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_rol")]
    public int IdRol { get; set; }

    [Column("fecha_asignacion")]
    public DateTime FechaAsignacion { get; set; } = DateTime.Now;

    public Usuario Usuario { get; set; } = null!;
    public Rol Rol { get; set; } = null!;
}
