namespace CuatroPatas.API.DTOs.Veterinario;

public class VeterinarioResponse
{
    public int IdVeterinario { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string NombreCompleto => $"{Nombre} {Apellido}";
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Especialidad { get; set; }
    public DateTime FechaRegistro { get; set; }
}
