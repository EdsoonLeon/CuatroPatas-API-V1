using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Medicamento;

public class StockRequest
{
    [Required, Range(1, int.MaxValue)]
    public int Cantidad { get; set; }
}
