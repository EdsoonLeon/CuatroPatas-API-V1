namespace CuatroPatas.API.Models.SpResults;

public class PrescripcionSpResult
{
    public int id_prescripcion { get; set; }
    public int id_historial { get; set; }
    public int id_medicamento { get; set; }
    public string? nombre_medicamento { get; set; }
    public string? dosis { get; set; }
    public string? frecuencia { get; set; }
    public string? duracion { get; set; }
    public int cantidad { get; set; }
    public DateTime fecha_prescripcion { get; set; }
}
