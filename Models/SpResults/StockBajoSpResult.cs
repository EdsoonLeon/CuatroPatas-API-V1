namespace CuatroPatas.API.Models.SpResults;

public class StockBajoSpResult
{
    public int id_medicamento { get; set; }
    public string? nombre { get; set; }
    public int stock { get; set; }
    public int stock_minimo { get; set; }
    public decimal precio { get; set; }
}
