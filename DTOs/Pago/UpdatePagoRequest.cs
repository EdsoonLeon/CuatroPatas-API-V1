using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Pago;

public class UpdatePagoRequest
{
    [Required]
    public string EstadoPago { get; set; } = string.Empty;

    public string? MetodoPago { get; set; }
    public DateTime? FechaPago { get; set; }
}
