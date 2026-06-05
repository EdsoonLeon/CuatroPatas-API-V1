// ═══════════════════════════════════════════════════════
// ARCHIVO: CancelCitaRequest.cs
// QUÉ HACE: Datos para cancelar una cita. Se exige un motivo de al menos 5 caracteres
//           para dejar registro de por qué se canceló — útil para el historial y estadísticas.
// QUIÉN LO USA: CitaController.Cancel → CitaService.CancelarCitaAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Cita;

public class CancelCitaRequest
{
    [Required, MinLength(5)]
    public string Motivo { get; set; } = string.Empty;
}
