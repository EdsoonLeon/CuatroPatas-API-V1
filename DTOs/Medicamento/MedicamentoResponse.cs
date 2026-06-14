namespace CuatroPatas.API.DTOs.Medicamento;

public class MedicamentoResponse
{
    public int IdMedicamento { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? Tipo { get; set; }
    public string? Presentacion { get; set; }
    public int Stock { get; set; }
    public decimal PrecioUnitario { get; set; }
}
