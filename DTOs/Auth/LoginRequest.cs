
// ARCHIVO: LoginRequest.cs
// QUÉ HACE: Datos que el usuario envía para iniciar sesión.
//           Solo necesita email y contraseña — nada más viaja por la red.
// QUIÉN LO USA: AuthController.Login → AuthService.LoginAsync


using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Auth;

public class LoginRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    // Contraseña en texto plano — AuthService la compara contra el hash BCrypt guardado en BD.
    // Nunca se almacena ni se loguea; desaparece después de la verificación.
    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
