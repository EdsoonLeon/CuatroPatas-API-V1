using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Servicio;

public class CreateServicioRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required, Range(0.01, double.MaxValue)]
    public decimal Precio { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int DuracionMinutos { get; set; }
}
