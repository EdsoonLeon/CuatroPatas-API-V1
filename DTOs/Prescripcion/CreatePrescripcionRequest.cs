using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Prescripcion;

public class CreatePrescripcionRequest
{
    [Required]
    public int IdHistorial { get; set; }

    [Required]
    public int IdMedicamento { get; set; }

    [Required]
    public string Dosis { get; set; } = string.Empty;

    [Required]
    public string Frecuencia { get; set; } = string.Empty;

    [Required]
    public string Duracion { get; set; } = string.Empty;

    [Required, Range(1, int.MaxValue)]
    public int Cantidad { get; set; }
}
