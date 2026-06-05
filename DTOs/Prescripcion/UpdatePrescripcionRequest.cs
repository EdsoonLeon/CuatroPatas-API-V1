// ═══════════════════════════════════════════════════════
// ARCHIVO: UpdatePrescripcionRequest.cs
// QUÉ HACE: Datos para modificar una receta existente (ajuste de dosis o duración).
//           No permite cambiar el medicamento — para eso hay que crear una nueva prescripción
//           y anular la anterior, para mantener trazabilidad del historial clínico.
// QUIÉN LO USA: PrescripcionController.Update → PrescripcionService.ActualizarPrescripcionAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Prescripcion;

public class UpdatePrescripcionRequest
{
    [Required]
    public string Dosis { get; set; } = string.Empty;

    [Required]
    public string Frecuencia { get; set; } = string.Empty;

    [Required]
    public string Duracion { get; set; } = string.Empty;

    [Required, Range(1, int.MaxValue)]
    public int Cantidad { get; set; }
}
