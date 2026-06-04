namespace CuatroPatas.API.Models.SpResults;

public class HistorialSpResult
{
    public int id_historial { get; set; }
    public int id_mascota { get; set; }
    public string? nombre_mascota { get; set; }
    public int id_veterinario { get; set; }
    public string? nombre_veterinario { get; set; }
    public string? apellido_veterinario { get; set; }
    public int? id_cita { get; set; }
    public string? tipo_registro { get; set; }
    public string? descripcion { get; set; }
    public string? diagnostico { get; set; }
    public string? tratamiento { get; set; }
    public DateTime fecha_registro { get; set; }
}
