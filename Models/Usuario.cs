// ═══════════════════════════════════════════════════════
// ARCHIVO: Usuario.cs
// QUÉ HACE: Representa la tabla USUARIO de la base de datos.
//           Guarda las credenciales de acceso y el estado de seguridad de cada cuenta.
//           NO guarda contraseñas en texto plano — solo el hash encriptado.
// QUIÉN LO USA: AuthService (login/registro), UsuarioRepository
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("USUARIO")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("nombre_usuario")]
    public string NombreUsuario { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    // La contraseña NUNCA se guarda en texto plano.
    // BCrypt la convierte en una "huella digital" que no se puede revertir.
    // Así, si hackean la base de datos, solo ven cadenas indescifrables.
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("activo")]
    public bool Activo { get; set; } = true;

    // Contador de veces que el usuario escribió la contraseña incorrecta.
    // El Stored Procedure sp_Usuario_RegisterFailedLogin lo incrementa en cada fallo.
    // Cuando llega al límite configurado, activa automáticamente el campo Bloqueado.
    [Column("intentos_fallidos")]
    public int IntentosFallidos { get; set; } = 0;

    // Si es true, el usuario NO puede iniciar sesión aunque tenga la contraseña correcta.
    // Se activa automáticamente tras demasiados intentos fallidos (protección contra fuerza bruta).
    // Solo un administrador puede desbloquearlo manualmente.
    [Column("bloqueado")]
    public bool Bloqueado { get; set; } = false;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // Relaciones de navegación — permiten acceder a los datos relacionados sin consultas extra
    public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
}
