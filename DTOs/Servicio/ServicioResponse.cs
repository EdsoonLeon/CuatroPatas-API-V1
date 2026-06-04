namespace CuatroPatas.API.DTOs.Servicio;

public class ServicioResponse
{
    public int IdServicio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int DuracionMinutos { get; set; }
}
