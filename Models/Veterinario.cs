// ═══════════════════════════════════════════════════════
// ARCHIVO: Veterinario.cs
// QUÉ HACE: Representa la tabla VETERINARIO de la base de datos.
//           Guarda el perfil profesional del veterinario.
//           Siempre está ligado a una cuenta de usuario para poder iniciar sesión,
//           aunque la FK es nullable por diseño del esquema SQL.
// QUIÉN LO USA: VeterinarioService, VeterinarioRepository, CitaService
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("VETERINARIO")]
public class Veterinario
{
    [Key]
    [Column("id_veterinario")]
    public int IdVeterinario { get; set; }

    // FK hacia USUARIO — nullable solo por flexibilidad del esquema,
    // pero en la práctica siempre se crea junto con su cuenta de usuario.
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
    // Sus franjas horarias de disponibilidad semanal
    public ICollection<HorarioVeterinario> Horarios { get; set; } = new List<HorarioVeterinario>();
    // Todas las citas asignadas a este veterinario
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
