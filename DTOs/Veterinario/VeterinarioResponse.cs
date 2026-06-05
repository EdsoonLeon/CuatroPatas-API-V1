// ═══════════════════════════════════════════════════════
// ARCHIVO: VeterinarioResponse.cs
// QUÉ HACE: Datos de un veterinario para mostrar en el frontend.
//           NombreCompleto está calculado en el DTO para comodidad del frontend —
//           no tiene que concatenar Nombre + Apellido en cada componente de React.
// QUIÉN LO USA: VeterinarioController en todas las respuestas de lectura
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Veterinario;

public class VeterinarioResponse
{
    public int IdVeterinario { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    // Calculado en el DTO — evita duplicar la concatenación en cada componente de React
    public string NombreCompleto => $"{Nombre} {Apellido}";
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Especialidad { get; set; }
    public DateTime FechaRegistro { get; set; }
}
