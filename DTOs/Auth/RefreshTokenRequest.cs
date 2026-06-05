// ═══════════════════════════════════════════════════════
// ARCHIVO: RefreshTokenRequest.cs
// QUÉ HACE: Datos que el cliente envía para renovar su AccessToken expirado.
//           El flujo es: AccessToken expira → el frontend envía este request →
//           el servidor valida el RefreshToken → emite un nuevo par de tokens
//           (AccessToken + RefreshToken) → revoca el anterior (rotación de token).
// QUIÉN LO USA: AuthController.RefreshToken → AuthService.RefreshTokenAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Auth;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
