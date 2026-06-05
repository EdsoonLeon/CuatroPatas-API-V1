// ═══════════════════════════════════════════════════════
// ARCHIVO: MeResponse.cs
// QUÉ HACE: Datos del usuario actualmente autenticado.
//           El frontend llama a GET /api/auth/me cuando recarga la página
//           para recuperar los datos de sesión sin tener que hacer login de nuevo.
//           Solo retorna información pública — nunca hash de contraseña ni tokens.
// QUIÉN LO USA: AuthController.Me → AuthService.ObtenerMeAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Auth;

public class MeResponse
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    // Roles determinan qué menús y acciones puede ver el usuario en el frontend
    public string[] Roles { get; set; } = [];
}
