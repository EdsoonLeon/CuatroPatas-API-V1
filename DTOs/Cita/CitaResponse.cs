namespace CuatroPatas.API.DTOs.Cita;

public class CitaResponse
{
    public int IdCita { get; set; }
    public int IdMascota { get; set; }
    public string NombreMascota { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public int IdVeterinario { get; set; }
    public string NombreVeterinario { get; set; } = string.Empty;
    public string ApellidoVeterinario { get; set; } = string.Empty;
    public int? IdCliente { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string ApellidoCliente { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string Motivo { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
    public DateTime FechaCreacion { get; set; }
}
