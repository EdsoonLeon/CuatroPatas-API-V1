namespace CuatroPatas.API.Models.SpResults;

public class EstadisticasSpResult
{
    public int total_citas { get; set; }
    public int citas_completadas { get; set; }
    public int citas_canceladas { get; set; }
    public int citas_pendientes { get; set; }
    public decimal ingresos_total { get; set; }
    public string? periodo { get; set; }
}
