namespace CuatroPatas.API.DTOs.Pago;

public class PagoResponse
{
    public int IdPago { get; set; }
    public int IdCita { get; set; }
    public decimal MontoTotal { get; set; }
    public string EstadoPago { get; set; } = string.Empty;
    public string? MetodoPago { get; set; }
    public DateTime? FechaPago { get; set; }
    public DateTime FechaCreacion { get; set; }
}
