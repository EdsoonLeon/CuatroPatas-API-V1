namespace CuatroPatas.API.DTOs.Veterinario;

public class AgendaResponse
{
    public int IdCita { get; set; }
    public DateTime FechaHora { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string Motivo { get; set; } = string.Empty;
    public string NombreMascota { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string NombreCliente { get; set; } = string.Empty;
    public string ApellidoCliente { get; set; } = string.Empty;
    public string? TelefonoCliente { get; set; }
}
