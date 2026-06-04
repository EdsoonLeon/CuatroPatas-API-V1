using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Mascota;

public class UpdateMascotaRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string Especie { get; set; } = string.Empty;

    public string? Raza { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Sexo { get; set; }
    public string? Color { get; set; }
    public decimal? Peso { get; set; }
    public string? FotoUrl { get; set; }
}
