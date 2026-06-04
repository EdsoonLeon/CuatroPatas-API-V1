namespace CuatroPatas.API.Models.SpResults;

public class CitaSpResult
{
    public int id_cita { get; set; }
    public int id_mascota { get; set; }
    public string? nombre_mascota { get; set; }
    public string? especie { get; set; }
    public int id_veterinario { get; set; }
    public string? nombre_veterinario { get; set; }
    public string? apellido_veterinario { get; set; }
    public int? id_cliente { get; set; }
    public string? nombre_cliente { get; set; }
    public string? apellido_cliente { get; set; }
    public DateTime fecha_hora { get; set; }
    public string? estado { get; set; }
    public string? motivo { get; set; }
    public string? observaciones { get; set; }
    public DateTime fecha_creacion { get; set; }
}
