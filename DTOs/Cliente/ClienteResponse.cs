// ═══════════════════════════════════════════════════════
// ARCHIVO: ClienteResponse.cs
// QUÉ HACE: Datos de un cliente para mostrar en el frontend.
//           Incluye NombreCompleto como propiedad calculada para que el frontend
//           no tenga que concatenar Nombre + Apellido en cada vista.
// QUIÉN LO USA: ClienteController en todas las respuestas de lectura
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Cliente;

public class ClienteResponse
{
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    // Calculado en el DTO para no duplicar la lógica de concatenación en cada vista del frontend
    public string NombreCompleto => $"{Nombre} {Apellido}";
    public string Email { get; set; } = string.Empty;
    public string? Dni { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public DateTime FechaRegistro { get; set; }
}
