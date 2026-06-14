namespace CuatroPatas.API.Models.SpResults;

// Resultado de sp_HistorialMedico_ListByMascota y sp_Mascota_GetHistorial
public class HistorialSpResult
{
    public int id_historial { get; set; }
    public DateOnly fecha { get; set; }
    public string? tipo { get; set; }
    public string? diagnostico { get; set; }
    public string? tratamiento { get; set; }
    public decimal? peso { get; set; }
    public decimal? temperatura { get; set; }
    public string? veterinario_nombre { get; set; }
}
