using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Cita;

public class AddServicioCitaRequest
{
    [Required]
    public int IdServicio { get; set; }
}
