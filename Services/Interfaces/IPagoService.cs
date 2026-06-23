// ═══════════════════════════════════════════════════════
// ARCHIVO: IPagoService.cs
// QUÉ HACE: Contrato del servicio de pagos.
//           Interfaz mínima porque los pagos no se crean desde aquí —
//           el SP sp_Cita_Create los genera automáticamente al crear una cita.
//           Solo se consultan y se actualizan cuando el cliente cancela su deuda.
// QUIÉN LO USA: PagoController (inyectado como IPagoService)
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.DTOs.Pago;

namespace CuatroPatas.API.Services.Interfaces;

public interface IPagoService
{
    Task<PagoResponse> GetByCitaAsync(int idCita);
    Task<List<PagoClienteResponse>> GetByClienteNombreAsync(string nombre);
    Task<PagoResponse> UpdateAsync(int id, UpdatePagoRequest request);
}
