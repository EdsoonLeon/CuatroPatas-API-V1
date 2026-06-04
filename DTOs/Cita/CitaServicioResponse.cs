namespace CuatroPatas.API.DTOs.Cita;

public class CitaServicioResponse
{
    public int IdServicio { get; set; }
    public string NombreServicio { get; set; } = string.Empty;
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
