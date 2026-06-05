// ═══════════════════════════════════════════════════════
// ARCHIVO: CreateVeterinarioRequest.cs
// QUÉ HACE: Datos para registrar un nuevo veterinario en el sistema.
//           A diferencia de CreateClienteRequest, este incluye Password
//           porque el SP crea simultáneamente la cuenta de Usuario con acceso al sistema.
// QUIÉN LO USA: VeterinarioController.Create → VeterinarioService.CrearVeterinarioAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Veterinario;

public class CreateVeterinarioRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, MinLength(2)]
    public string Apellido { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? Telefono { get; set; }
    public string? Especialidad { get; set; }

    // Contraseña inicial — el SP la hashea con BCrypt antes de guardarla.
    // El admin debería informarla al veterinario para que la cambie en su primer acceso.
    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
