namespace CuatroPatas.API.Models.SpResults;

// Resultado de sp_Cita_Read y sp_Cita_List / sp_Cita_ListToday
public class CitaSpResult
{
    public int id_cita { get; set; }
    public int id_mascota { get; set; }
    public int id_veterinario { get; set; }
    public int? id_cliente { get; set; }
    public DateTime fecha_hora { get; set; }
    public string? motivo { get; set; }
    public string? estado { get; set; }
    public string? observaciones { get; set; }
    public string? mascota_nombre { get; set; }
    public string? mascota_especie { get; set; }
    public string? veterinario_nombre { get; set; }
    public string? cliente_nombre { get; set; }
}
