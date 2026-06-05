// ═══════════════════════════════════════════════════════
// ARCHIVO: Rol.cs
// QUÉ HACE: Representa la tabla ROL de la base de datos.
//           Contiene los roles disponibles en el sistema:
//           Administrador, Veterinario, Recepcionista, Cliente.
// QUIÉN LO USA: RolRepository, AuthService al asignar roles
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("ROL")]
public class Rol
{
    [Key]
    [Column("id_rol")]
    public int IdRol { get; set; }

    [Column("nombre_rol")]
    public string NombreRol { get; set; } = string.Empty;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
}
