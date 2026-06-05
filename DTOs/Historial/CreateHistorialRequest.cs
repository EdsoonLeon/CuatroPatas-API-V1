// ═══════════════════════════════════════════════════════
// ARCHIVO: CreateHistorialRequest.cs
// QUÉ HACE: Datos para crear un nuevo registro en el historial médico.
//           IdCita es opcional — permite crear registros clínicos
//           sin una cita agendada (urgencias, vacunaciones directas, notas de seguimiento).
// QUIÉN LO USA: HistorialController.Create → HistorialService.CrearHistorialAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Historial;

public class CreateHistorialRequest
{
    [Required]
    public int IdMascota { get; set; }

    [Required]
    public int IdVeterinario { get; set; }

    // Opcional — si el registro viene de una cita agendada, se vincula aquí
    public int? IdCita { get; set; }

    // Categoría del registro: "Consulta", "Vacunación", "Cirugía", "Examen", etc.
    [Required]
    public string TipoRegistro { get; set; } = string.Empty;

    [Required, MinLength(5)]
    public string Descripcion { get; set; } = string.Empty;

    public string? Diagnostico { get; set; }
    public string? Tratamiento { get; set; }
}
