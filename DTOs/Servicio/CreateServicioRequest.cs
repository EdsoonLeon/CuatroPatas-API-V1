// ═══════════════════════════════════════════════════════
// ARCHIVO: CreateServicioRequest.cs
// QUÉ HACE: Datos para agregar un nuevo servicio al catálogo de la clínica.
//           DuracionMinutos se usa para calcular disponibilidad de la agenda del veterinario.
// QUIÉN LO USA: ServicioController.Create → ServicioService.CrearServicioAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Servicio;

public class CreateServicioRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required, Range(0.01, double.MaxValue)]
    public decimal Precio { get; set; }

    // Tiempo estimado en minutos — ayuda a calcular si hay espacio en la agenda del veterinario
    [Required, Range(1, int.MaxValue)]
    public int DuracionMinutos { get; set; }
}
