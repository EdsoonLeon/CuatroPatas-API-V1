namespace CuatroPatas.API.Models.SpResults;

public class CitaServicioSpResult
{
    public int id_servicio { get; set; }
    public string? nombre_servicio { get; set; }
    public decimal precio_unitario { get; set; }
    public decimal subtotal { get; set; }
}
