namespace CuatroPatas.API.DTOs.Historial;

public class HistorialResponse
{
    public int IdHistorial { get; set; }
    public int IdMascota { get; set; }
    public int IdVeterinario { get; set; }
    public int? IdCita { get; set; }
    public DateOnly Fecha { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string? Diagnostico { get; set; }
    public string? Tratamiento { get; set; }
    public string? Observaciones { get; set; }
    public decimal? Peso { get; set; }
    public decimal? Temperatura { get; set; }
    public string NombreVeterinario { get; set; } = string.Empty;
}
