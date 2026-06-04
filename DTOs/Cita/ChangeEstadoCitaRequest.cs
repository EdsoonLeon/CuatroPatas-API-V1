using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Cita;

public class ChangeEstadoCitaRequest
{
    [Required]
    public string Estado { get; set; } = string.Empty;
}
