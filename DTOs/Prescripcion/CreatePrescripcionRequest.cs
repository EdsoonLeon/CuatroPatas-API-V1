// ═══════════════════════════════════════════════════════
// ARCHIVO: CreatePrescripcionRequest.cs
// QUÉ HACE: Datos para recetar un medicamento en un historial médico.
//           Al crear la prescripción, el SP descuenta automáticamente
//           la Cantidad del stock del medicamento en la tabla MEDICAMENTO.
// QUIÉN LO USA: PrescripcionController.Create → PrescripcionService.CrearPrescripcionAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Prescripcion;

public class CreatePrescripcionRequest
{
    [Required]
    public int IdHistorial { get; set; }

    [Required]
    public int IdMedicamento { get; set; }

    // Ejemplo: "10 mg", "2 tabletas", "5 ml"
    [Required]
    public string Dosis { get; set; } = string.Empty;

    // Ejemplo: "cada 8 horas", "una vez al día"
    [Required]
    public string Frecuencia { get; set; } = string.Empty;

    // Ejemplo: "7 días", "2 semanas"
    [Required]
    public string Duracion { get; set; } = string.Empty;

    // Total de unidades a despachar — el SP descuenta este número del stock
    [Required, Range(1, int.MaxValue)]
    public int Cantidad { get; set; }
}
