namespace CuatroPatas.API.DTOs.Mascota;

public class MascotaResponse
{
    public int IdMascota { get; set; }
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string? Raza { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    // Calculada por SQL Server a partir de FechaNacimiento — la app nunca la escribe
    public int? EdadCalculada { get; set; }
    public string? Sexo { get; set; }
    public string? Color { get; set; }
    public decimal? Peso { get; set; }
    public string? NumeroChip { get; set; }
}
