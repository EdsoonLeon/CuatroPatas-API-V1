namespace CuatroPatas.API.DTOs.Historial;

public class HistorialResponse
{
    public int IdHistorial { get; set; }
    public int IdMascota { get; set; }
    public string NombreMascota { get; set; } = string.Empty;
    public int IdVeterinario { get; set; }
    public string NombreVeterinario { get; set; } = string.Empty;
    public string ApellidoVeterinario { get; set; } = string.Empty;
    public int? IdCita { get; set; }
    public string TipoRegistro { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string? Diagnostico { get; set; }
    public string? Tratamiento { get; set; }
    public DateTime FechaRegistro { get; set; }
}
