using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Medicamento;

public class CreateMedicamentoRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Required, Range(0.01, double.MaxValue)]
    public decimal Precio { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int StockMinimo { get; set; } = 5;
}
