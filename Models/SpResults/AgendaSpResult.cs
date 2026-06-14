namespace CuatroPatas.API.Models.SpResults;

// Resultado de sp_Veterinario_GetAgenda
public class AgendaSpResult
{
    public int id_cita { get; set; }
    public DateTime fecha_hora { get; set; }
    public int duracion_minutos { get; set; }
    public string? motivo { get; set; }
    public string? estado { get; set; }
    public string? mascota_nombre { get; set; }
    public string? especie { get; set; }
    public string? cliente_nombre { get; set; }
    public string? cliente_telefono { get; set; }
}
