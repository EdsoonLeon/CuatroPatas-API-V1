using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Cita;

public class CancelCitaRequest
{
    [Required, MinLength(5)]
    public string Motivo { get; set; } = string.Empty;
}
