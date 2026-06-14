namespace CuatroPatas.API.Models.SpResults;

// Resultado de sp_Medicamento_ListStockBajo
public class StockBajoSpResult
{
    public int id_medicamento { get; set; }
    public string? nombre { get; set; }
    public string? tipo { get; set; }
    public int stock { get; set; }
    public decimal precio_unitario { get; set; }
}
