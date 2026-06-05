// ═══════════════════════════════════════════════════════
// ARCHIVO: AddServicioCitaRequest.cs
// QUÉ HACE: Datos para agregar un servicio a una cita existente.
//           El SP sp_Cita_AddServicio copia el precio actual del servicio
//           a DetalleCita.PrecioUnitario para congelarlo en el tiempo.
// QUIÉN LO USA: CitaController.AddServicio → CitaService.AgregarServicioAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Cita;

public class AddServicioCitaRequest
{
    [Required]
    public int IdServicio { get; set; }
}
