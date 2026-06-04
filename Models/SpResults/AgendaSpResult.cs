namespace CuatroPatas.API.Models.SpResults;

public class AgendaSpResult
{
    public int id_cita { get; set; }
    public DateTime fecha_hora { get; set; }
    public string? estado { get; set; }
    public string? motivo { get; set; }
    public string? nombre_mascota { get; set; }
    public string? especie { get; set; }
    public string? nombre_cliente { get; set; }
    public string? apellido_cliente { get; set; }
    public string? telefono_cliente { get; set; }
}
