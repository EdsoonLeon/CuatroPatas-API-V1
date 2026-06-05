// ═══════════════════════════════════════════════════════
// ARCHIVO: StockBajoSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Medicamento_ListStockBajo.
//           Devuelve solo los medicamentos cuyo stock actual es ≤ al stock mínimo configurado.
//           Es la alerta de inventario: el admin ve esta lista y sabe qué debe reponer.
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: MedicamentoRepository.ObtenerStockBajoAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ Las propiedades DEBEN ser snake_case para que EF las mapee correctamente desde FromSqlRaw
public class StockBajoSpResult
{
    public int id_medicamento { get; set; }
    public string? nombre { get; set; }
    // Unidades disponibles actualmente — es ≤ stock_minimo para aparecer en este reporte
    public int stock { get; set; }
    // El umbral configurado en la tabla MEDICAMENTO — si stock cae por debajo, aparece aquí
    public int stock_minimo { get; set; }
    public decimal precio { get; set; }
}
