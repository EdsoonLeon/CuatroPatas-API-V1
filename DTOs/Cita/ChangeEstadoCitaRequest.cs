// ═══════════════════════════════════════════════════════
// ARCHIVO: ChangeEstadoCitaRequest.cs
// QUÉ HACE: Permite cambiar el estado de una cita manualmente.
//           El ciclo normal es: Pendiente → En Curso → Completada.
//           Para cancelar se usa CancelCitaRequest que exige motivo.
//           Solo el personal de clínica (vet o admin) puede usar este endpoint.
// QUIÉN LO USA: CitaController.ChangeEstado → CitaService.CambiarEstadoAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Cita;

public class ChangeEstadoCitaRequest
{
    // Valores válidos: "Pendiente", "En Curso", "Completada"
    [Required]
    public string Estado { get; set; } = string.Empty;
}
