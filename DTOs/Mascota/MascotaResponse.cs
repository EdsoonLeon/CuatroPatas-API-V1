// ═══════════════════════════════════════════════════════
// ARCHIVO: MascotaResponse.cs
// QUÉ HACE: Datos de una mascota para mostrar en el frontend.
//           EdadCalculada viene de SQL Server — la app solo la lee y la propaga aquí.
// QUIÉN LO USA: MascotaController en todas las respuestas de lectura
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Mascota;

public class MascotaResponse
{
    public int IdMascota { get; set; }
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string? Raza { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    // Calculada por SQL Server a partir de FechaNacimiento — la app nunca la escribe
    public int? EdadCalculada { get; set; }
    public string? Sexo { get; set; }
    public string? Color { get; set; }
    public decimal? Peso { get; set; }
    public string? FotoUrl { get; set; }
}
