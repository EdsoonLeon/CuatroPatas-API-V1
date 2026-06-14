namespace CuatroPatas.API.Models.SpResults;

// Resultado de sp_Prescripcion_ListByHistorial y sp_Prescripcion_ListByMascota
public class PrescripcionSpResult
{
    public int id_prescripcion { get; set; }
    public int? id_medicamento { get; set; }
    public string? medicamento_nombre { get; set; }
    public string? dosis { get; set; }
    public string? frecuencia { get; set; }
    public int? duracion_dias { get; set; }
    public string? indicaciones { get; set; }
    public DateOnly? fecha_inicio { get; set; }
    public DateOnly? fecha_fin { get; set; }
    // Solo presente en sp_Prescripcion_ListByMascota
    public DateOnly? fecha_historial { get; set; }
    public string? veterinario_nombre { get; set; }
}
