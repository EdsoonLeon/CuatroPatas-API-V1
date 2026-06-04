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

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
