// ═══════════════════════════════════════════════════════
// ARCHIVO: LogoutRequest.cs
// QUÉ HACE: Datos que el cliente envía para cerrar sesión.
//           Al hacer logout, el RefreshToken se revoca en la BD para que nadie
//           pueda usarlo para generar nuevos AccessTokens, aunque lo haya robado.
//           El AccessToken sigue siendo válido hasta que expire (máx 60 min) —
//           no hay forma de invalidarlo antes sin una lista negra centralizada.
// QUIÉN LO USA: AuthController.Logout → AuthService.LogoutAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Auth;

public class LogoutRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
