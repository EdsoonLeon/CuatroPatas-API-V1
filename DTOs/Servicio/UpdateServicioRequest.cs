// ═══════════════════════════════════════════════════════
// ARCHIVO: UpdateServicioRequest.cs
// QUÉ HACE: Datos para actualizar un servicio del catálogo.
//           Si se cambia el Precio, las citas futuras usarán el nuevo precio.
//           Las citas pasadas mantienen su precio original (guardado en DetalleCita).
// QUIÉN LO USA: ServicioController.Update → ServicioService.ActualizarServicioAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Servicio;

public class UpdateServicioRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required, Range(0.01, double.MaxValue)]
    public decimal Precio { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int DuracionMinutos { get; set; }
}
