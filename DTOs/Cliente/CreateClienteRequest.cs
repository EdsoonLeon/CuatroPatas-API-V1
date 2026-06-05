// ═══════════════════════════════════════════════════════
// ARCHIVO: CreateClienteRequest.cs
// QUÉ HACE: Datos para registrar un nuevo cliente directamente por el admin.
//           A diferencia del registro público (RegisterRequest), este crea solo
//           el Cliente sin crear una cuenta de acceso al sistema.
//           El SP sp_Cliente_Create puede crear también un usuario si se desea.
// QUIÉN LO USA: ClienteController.Create → ClienteService.CrearClienteAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Cliente;

public class CreateClienteRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, MinLength(2)]
    public string Apellido { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
}
