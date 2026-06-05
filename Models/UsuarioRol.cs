// ═══════════════════════════════════════════════════════
// ARCHIVO: UsuarioRol.cs
// QUÉ HACE: Representa la tabla intermedia USUARIO_ROL.
//           Es la tabla que conecta usuarios con roles (relación muchos a muchos).
//           Un usuario puede tener varios roles y un rol puede pertenecer a muchos usuarios.
//           Esta tabla NO tiene un ID propio — su clave primaria son las dos FKs juntas.
// QUIÉN LO USA: AppDbContext (configura la clave compuesta), AuthService
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

// La clave primaria compuesta (IdUsuario + IdRol) se define en AppDbContext.OnModelCreating(),
// no aquí con atributos, porque EF no soporta claves compuestas con [Key] directamente.
[Table("USUARIO_ROL")]
public class UsuarioRol
{
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("id_rol")]
    public int IdRol { get; set; }

    [Column("fecha_asignacion")]
    public DateTime FechaAsignacion { get; set; } = DateTime.Now;

    // Propiedades de navegación para acceder al Usuario y Rol completos si se hace Include()
    public Usuario Usuario { get; set; } = null!;
    public Rol Rol { get; set; } = null!;
}
