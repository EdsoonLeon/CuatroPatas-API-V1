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
