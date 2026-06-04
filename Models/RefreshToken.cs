using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("REFRESH_TOKEN")]
public class RefreshToken
{
    [Key]
    [Column("id_token")]
    public int IdToken { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("token")]
    public string Token { get; set; } = string.Empty;

    [Column("fecha_expiracion")]
    public DateTime FechaExpiracion { get; set; }

    [Column("revocado")]
    public bool Revocado { get; set; } = false;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public Usuario Usuario { get; set; } = null!;
}
