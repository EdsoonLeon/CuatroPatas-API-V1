// ═══════════════════════════════════════════════════════
// ARCHIVO: IPagoRepository.cs
// QUÉ HACE: Contrato del "cajero" que habla con la tabla PAGO.
//           No tiene Create porque el pago lo crea automáticamente el SP sp_Cita_Create.
//           Solo permite leer y actualizar (cuando el cliente paga).
// QUIÉN LO USA: PagoRepository (implementación), PagoService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IPagoRepository
{
    Task<Pago?> GetByCitaIdAsync(int idCita);
    Task<Pago?> GetByIdAsync(int id);
    Task<List<(Pago Pago, string NombreCliente)>> GetByClienteNombreAsync(string nombre);
    Task<Pago> UpdateAsync(Pago pago);
}
