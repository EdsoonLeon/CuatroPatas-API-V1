namespace CuatroPatas.API.DTOs.Cita;

public class CitaFilterRequest
{
    public int? IdVeterinario { get; set; }
    public int? IdCliente { get; set; }
    public int? IdMascota { get; set; }
    public string? Estado { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
}
