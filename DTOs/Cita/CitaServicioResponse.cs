// ═══════════════════════════════════════════════════════
// ARCHIVO: CitaServicioResponse.cs
// QUÉ HACE: Detalle de un servicio incluido en una cita, con el precio
//           al que fue cobrado (que puede diferir del precio actual del catálogo).
//           Es la "línea de factura" que el frontend muestra al ver una cita.
// QUIÉN LO USA: CitaController.GetServicios
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Cita;

public class CitaServicioResponse
{
    public int IdServicio { get; set; }
    public string NombreServicio { get; set; } = string.Empty;
    // Precio en el momento de la cita — "congelado", no el precio actual
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
