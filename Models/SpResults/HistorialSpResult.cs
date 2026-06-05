// ═══════════════════════════════════════════════════════
// ARCHIVO: HistorialSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Mascota_GetHistorial
//           y sp_HistorialMedico_ListByMascota.
//           Devuelve el historial clínico con los nombres del veterinario y la mascota
//           ya incluidos (desnormalizados) para que el frontend los muestre sin joins extra.
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: HistorialRepository.ObtenerHistorialAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ Las propiedades DEBEN ser snake_case para que EF las mapee correctamente desde FromSqlRaw
public class HistorialSpResult
{
    public int id_historial { get; set; }
    public int id_mascota { get; set; }
    public string? nombre_mascota { get; set; }
    public int id_veterinario { get; set; }
    public string? nombre_veterinario { get; set; }
    public string? apellido_veterinario { get; set; }
    // Null si el historial no está asociado a una cita agendada
    public int? id_cita { get; set; }
    public string? tipo_registro { get; set; }
    public string? descripcion { get; set; }
    public string? diagnostico { get; set; }
    public string? tratamiento { get; set; }
    public DateTime fecha_registro { get; set; }
}
