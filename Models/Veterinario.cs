using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("VETERINARIO")]
public class Veterinario
{
    [Key]
    [Column("id_veterinario")]
    public int IdVeterinario { get; set; }

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

    [Column("especialidad")]
    public string? Especialidad { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public Usuario? Usuario { get; set; }
    public ICollection<HorarioVeterinario> Horarios { get; set; } = new List<HorarioVeterinario>();
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
