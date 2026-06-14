using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Medicamento;

public class CreateMedicamentoRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public string? Tipo { get; set; }

    public string? Presentacion { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Required, Range(0.01, double.MaxValue)]
    public decimal PrecioUnitario { get; set; }
}
