// ═══════════════════════════════════════════════════════
// ARCHIVO: RegisterRequest.cs
// QUÉ HACE: Datos para crear una cuenta de cliente desde el portal público.
//           En una sola operación, el SP crea: una cuenta de Usuario (con contraseña
//           hasheada por BCrypt), un registro de Cliente, y asigna el rol "Cliente".
// QUIÉN LO USA: AuthController.Register → AuthService.RegisterAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Auth;

public class RegisterRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, MinLength(2)]
    public string Apellido { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    // Contraseña en texto plano — el SP la hashea con BCrypt (workFactor: 12) antes de guardarla.
    // Nunca se almacena sin hashear.
    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;

    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
}
