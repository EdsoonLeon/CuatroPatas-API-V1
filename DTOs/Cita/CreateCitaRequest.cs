// ═══════════════════════════════════════════════════════
// ARCHIVO: CreateCitaRequest.cs
// QUÉ HACE: Datos necesarios para agendar una nueva cita.
//           El SP sp_Cita_Create usa estos datos para crear la cita
//           y automáticamente genera el registro de Pago asociado.
// QUIÉN LO USA: CitaController.Create → CitaService.CrearCitaAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Cita;

public class CreateCitaRequest
{
    [Required]
    public int IdMascota { get; set; }

    [Required]
    public int IdVeterinario { get; set; }

    [Required]
    public DateTime FechaHora { get; set; }

    [Required, MinLength(5)]
    public string Motivo { get; set; } = string.Empty;

    public string? Observaciones { get; set; }
}
