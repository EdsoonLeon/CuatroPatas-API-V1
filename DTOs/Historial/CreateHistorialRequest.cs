using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Historial;

public class CreateHistorialRequest
{
    [Required]
    public int IdMascota { get; set; }

    [Required]
    public int IdVeterinario { get; set; }

    public int? IdCita { get; set; }

    [Required]
    public string TipoRegistro { get; set; } = string.Empty;

    [Required, MinLength(5)]
    public string Descripcion { get; set; } = string.Empty;

    public string? Diagnostico { get; set; }
    public string? Tratamiento { get; set; }
}
