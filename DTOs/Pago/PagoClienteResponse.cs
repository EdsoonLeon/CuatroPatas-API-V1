namespace CuatroPatas.API.DTOs.Pago;

public class PagoClienteResponse
{
    public int IdPago { get; set; }
    public int IdCita { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public decimal MontoTotal { get; set; }
    public string EstadoPago { get; set; } = string.Empty;
    public string? MetodoPago { get; set; }
    public DateTime? FechaPago { get; set; }
}
