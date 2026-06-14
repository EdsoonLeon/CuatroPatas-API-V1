// ═══════════════════════════════════════════════════════
// ARCHIVO: CitaResponse.cs
// QUÉ HACE: Vista completa de una cita con todos los datos relacionados incluidos.
//           Combina datos de CITA, MASCOTA, VETERINARIO y CLIENTE en un solo objeto
//           para que el frontend no necesite hacer peticiones adicionales.
//           AutoMapper convierte CitaSpResult → CitaResponse.
// QUIÉN LO USA: CitaController en todas las respuestas de lectura
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Cita;

public class CitaResponse
{
    public int IdCita { get; set; }
    public int IdMascota { get; set; }
    public string NombreMascota { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public int IdVeterinario { get; set; }
    public string NombreVeterinario { get; set; } = string.Empty;
    // Null si el dueño de la mascota no tiene cuenta de cliente registrada
    public int? IdCliente { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    // "Pendiente", "En Curso", "Completada" o "Cancelada"
    public string Estado { get; set; } = string.Empty;
    public string Motivo { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
}
