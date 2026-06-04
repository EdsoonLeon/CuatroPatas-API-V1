namespace CuatroPatas.API.DTOs.Medicamento;

public class MedicamentoResponse
{
    public int IdMedicamento { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int Stock { get; set; }
    public decimal Precio { get; set; }
    public int StockMinimo { get; set; }
    public bool StockBajo => Stock <= StockMinimo;
}
