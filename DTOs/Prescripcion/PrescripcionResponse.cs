// ═══════════════════════════════════════════════════════
// ARCHIVO: PrescripcionResponse.cs
// QUÉ HACE: Datos de una receta médica para mostrar en el frontend.
//           Incluye el nombre del medicamento (no solo el ID) para que
//           el usuario pueda leerlo directamente sin consultas adicionales.
// QUIÉN LO USA: PrescripcionController en todas las respuestas de lectura
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Prescripcion;

public class PrescripcionResponse
{
    public int IdPrescripcion { get; set; }
    public int IdHistorial { get; set; }
    public int IdMedicamento { get; set; }
    // Nombre del medicamento del JOIN con la tabla MEDICAMENTO — más legible que solo el ID
    public string NombreMedicamento { get; set; } = string.Empty;
    public string Dosis { get; set; } = string.Empty;
    public string Frecuencia { get; set; } = string.Empty;
    public string Duracion { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public DateTime FechaPrescripcion { get; set; }
}
