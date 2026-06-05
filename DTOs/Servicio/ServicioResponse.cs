// ═══════════════════════════════════════════════════════
// ARCHIVO: ServicioResponse.cs
// QUÉ HACE: Datos de un servicio del catálogo para mostrar en el frontend.
//           Se usa tanto en el listado del catálogo como al mostrar los servicios de una cita.
// QUIÉN LO USA: ServicioController en todas las respuestas de lectura
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Servicio;

public class ServicioResponse
{
    public int IdServicio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int DuracionMinutos { get; set; }
}
