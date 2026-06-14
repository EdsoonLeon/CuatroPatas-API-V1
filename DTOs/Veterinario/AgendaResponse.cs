// ═══════════════════════════════════════════════════════
// ARCHIVO: AgendaResponse.cs
// QUÉ HACE: Vista del calendario del veterinario — una cita con los datos
//           del paciente y el dueño ya incluidos para mostrar en la agenda.
//           TelefonoCliente está incluido directamente para que el veterinario
//           pueda contactar al dueño sin navegar a otra pantalla.
// QUIÉN LO USA: VeterinarioController.GetAgenda
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Veterinario;

public class AgendaResponse
{
    public int IdCita { get; set; }
    public DateTime FechaHora { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string Motivo { get; set; } = string.Empty;
    public string NombreMascota { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string NombreCliente { get; set; } = string.Empty;
    public string? TelefonoCliente { get; set; }
}
