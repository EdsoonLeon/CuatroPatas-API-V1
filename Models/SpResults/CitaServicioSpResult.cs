namespace CuatroPatas.API.Models.SpResults;

// Resultado de sp_Cita_GetServicios
public class CitaServicioSpResult
{
    public int id_detalle { get; set; }
    public int id_servicio { get; set; }
    public string? servicio_nombre { get; set; }
    public int cantidad { get; set; }
    public decimal precio_unitario { get; set; }
    public decimal subtotal { get; set; }
}
