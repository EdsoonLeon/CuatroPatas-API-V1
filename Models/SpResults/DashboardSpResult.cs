namespace CuatroPatas.API.Models.SpResults;

public class DashboardSpResult
{
    public int total_citas_hoy { get; set; }
    public int total_citas_semana { get; set; }
    public int total_citas_mes { get; set; }
    public int total_clientes { get; set; }
    public int total_mascotas { get; set; }
    public int total_veterinarios { get; set; }
    public int citas_pendientes { get; set; }
    public int citas_en_curso { get; set; }
    public int citas_completadas_hoy { get; set; }
    public decimal ingresos_hoy { get; set; }
    public decimal ingresos_mes { get; set; }
    public int medicamentos_stock_bajo { get; set; }
}
