// ═══════════════════════════════════════════════════════
// ARCHIVO: LoginResponse.cs
// QUÉ HACE: Lo que el servidor devuelve después de un login o registro exitoso.
//           Incluye el "carnet digital" (AccessToken), el "código de renovación"
//           (RefreshToken), y los datos básicos del usuario para que el frontend
//           pueda inicializar la sesión sin necesitar otra petición.
// QUIÉN LO USA: AuthController.Login, AuthController.Register, AuthController.RefreshToken
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Auth;

public class LoginResponse
{
    // El JWT "carnet digital" — tiene vigencia de 60 minutos.
    // El frontend lo envía en cada request como: Authorization: Bearer <token>
    public string AccessToken { get; set; } = string.Empty;

    // El "código de renovación" — dura 7 días.
    // Cuando el AccessToken expire, el cliente lo usa para obtener un nuevo par de tokens
    // sin pedirle al usuario que vuelva a ingresar su contraseña.
    public string RefreshToken { get; set; } = string.Empty;

    // Fecha y hora exacta en que el AccessToken deja de ser válido
    public DateTime ExpiresAt { get; set; }

    public int IdUsuario { get; set; }
    public string Email { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
    // Lista de roles: ["Admin"], ["Cliente"], ["Veterinario", "Admin"], etc.
    public string[] Roles { get; set; } = [];
}
