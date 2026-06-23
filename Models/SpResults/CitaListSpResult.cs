namespace CuatroPatas.API.Models.SpResults;

// Resultado de sp_Cita_List y sp_Cita_ListToday
// Estos SPs no devuelven id_mascota/id_veterinario/id_cliente/observaciones,
// por eso existe esta clase separada de CitaSpResult (que es para sp_Cita_Read).
public class CitaListSpResult
{
    public int id_cita { get; set; }
    public DateTime fecha_hora { get; set; }
    public int duracion_minutos { get; set; }
    public string? motivo { get; set; }
    public string? estado { get; set; }
    public string? mascota_nombre { get; set; }
    public string? mascota_especie { get; set; }
    public string? veterinario_nombre { get; set; }
    public string? cliente_nombre { get; set; }
    public string? cliente_telefono { get; set; }
}
