// ═══════════════════════════════════════════════════════
// ARCHIVO: EstadisticasSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Veterinario_GetStats.
//           Muestra las métricas de rendimiento de un veterinario en un período:
//           cuántas citas tuvo, cuántas completó, cuántas cancelaron y cuánto generó.
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: VeterinarioRepository.ObtenerEstadisticasAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ Las propiedades DEBEN ser snake_case para que EF las mapee correctamente desde FromSqlRaw
public class EstadisticasSpResult
{
    public int total_citas { get; set; }
    public int citas_completadas { get; set; }
    public int citas_canceladas { get; set; }
    public int citas_pendientes { get; set; }
    // Suma de montos pagados en citas completadas durante el período consultado
    public decimal ingresos_total { get; set; }
    // Descripción textual del período filtrado: "Enero 2025", "2025", etc.
    public string? periodo { get; set; }
}
