// ═══════════════════════════════════════════════════════
// ARCHIVO: CitaServicioSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Cita_GetServicios.
//           Devuelve los servicios incluidos en una cita específica con sus precios.
//           Recuerda: PrecioUnitario refleja el precio al momento de la cita,
//           no el precio actual del catálogo (que puede haber cambiado después).
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: CitaRepository.ObtenerServiciosDeCitaAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ Las propiedades DEBEN ser snake_case para que EF las mapee correctamente desde FromSqlRaw
public class CitaServicioSpResult
{
    public int id_servicio { get; set; }
    public string? nombre_servicio { get; set; }
    // Precio "congelado" en el momento de la cita — no el precio actual del catálogo
    public decimal precio_unitario { get; set; }
    public decimal subtotal { get; set; }
}
