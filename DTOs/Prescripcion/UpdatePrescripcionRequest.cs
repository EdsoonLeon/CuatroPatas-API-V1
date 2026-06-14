using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Prescripcion;

public class UpdatePrescripcionRequest
{
    [Required]
    public string Dosis { get; set; } = string.Empty;

    public string? Frecuencia { get; set; }

    public int? DuracionDias { get; set; }

    public string? Indicaciones { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }
}
