// ═══════════════════════════════════════════════════════
// ARCHIVO: PrescripcionSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Prescripcion_ListByHistorial
//           y sp_Prescripcion_ListByMascota.
//           Devuelve las recetas médicas con el nombre del medicamento incluido,
//           para que el frontend pueda mostrarlos sin consultas adicionales.
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: PrescripcionRepository.ListarPorHistorialAsync, ListarPorMascotaAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ Las propiedades DEBEN ser snake_case para que EF las mapee correctamente desde FromSqlRaw
public class PrescripcionSpResult
{
    public int id_prescripcion { get; set; }
    public int id_historial { get; set; }
    public int id_medicamento { get; set; }
    // Nombre legible del medicamento — viene del JOIN con la tabla MEDICAMENTO
    public string? nombre_medicamento { get; set; }
    public string? dosis { get; set; }
    public string? frecuencia { get; set; }
    public string? duracion { get; set; }
    // Unidades despachadas — el SP usó este valor para descontar del stock al crear la prescripción
    public int cantidad { get; set; }
    public DateTime fecha_prescripcion { get; set; }
}
