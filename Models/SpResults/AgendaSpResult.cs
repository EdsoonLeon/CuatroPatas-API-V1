// ═══════════════════════════════════════════════════════
// ARCHIVO: AgendaSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Veterinario_GetAgenda.
//           Es la vista del calendario del veterinario: citas en un rango de fechas
//           con los datos del cliente y la mascota ya incluidos para mostrarse en pantalla.
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: VeterinarioRepository.ObtenerAgendaAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ Las propiedades DEBEN ser snake_case para que EF las mapee correctamente desde FromSqlRaw
public class AgendaSpResult
{
    public int id_cita { get; set; }
    public DateTime fecha_hora { get; set; }
    public string? estado { get; set; }
    public string? motivo { get; set; }
    public string? nombre_mascota { get; set; }
    public string? especie { get; set; }
    public string? nombre_cliente { get; set; }
    public string? apellido_cliente { get; set; }
    // Teléfono del cliente incluido directamente para que el vet pueda contactarlo sin navegar
    public string? telefono_cliente { get; set; }
}
