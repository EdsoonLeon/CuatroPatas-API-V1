// ═══════════════════════════════════════════════════════
// ARCHIVO: RefreshToken.cs
// QUÉ HACE: Representa la tabla REFRESH_TOKEN de la base de datos.
//           Guarda los "códigos de renovación" de sesión de cada usuario.
//           Cuando el JWT de 60 minutos expira, el frontend usa este token
//           para obtener un JWT nuevo sin pedir la contraseña de nuevo.
// QUIÉN LO USA: AuthService (login, refresh, logout), RefreshTokenRepository
// ═══════════════════════════════════════════════════════

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

    // El token en sí — una cadena larga y aleatoria generada con criptografía segura.
    // No contiene información del usuario; es solo una clave opaca que buscamos en la tabla.
    [Column("token")]
    public string Token { get; set; } = string.Empty;

    // Cuándo vence este token de renovación (normalmente 7 días después de crearlo)
    [Column("fecha_expiracion")]
    public DateTime FechaExpiracion { get; set; }

    // Se marca como true cuando el usuario hace logout, o cuando se rota el token
    // (al renovar el JWT se invalida el token viejo y se crea uno nuevo).
    // Esto evita que el mismo código de renovación se use más de una vez.
    [Column("revocado")]
    public bool Revocado { get; set; } = false;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // Propiedad de navegación — permite acceder al Usuario completo cuando se hace Include()
   
}
