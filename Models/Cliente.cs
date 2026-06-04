using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("CLIENTE")]
public class Cliente
{
    [Key]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("apellido")]
    public string Apellido { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("telefono")]
    public string? Telefono { get; set; }

    [Column("direccion")]
    public string? Direccion { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public Usuario? Usuario { get; set; }
    public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
}
