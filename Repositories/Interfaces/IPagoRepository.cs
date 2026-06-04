using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IPagoRepository
{
    Task<Pago?> GetByCitaIdAsync(int idCita);
    Task<Pago?> GetByIdAsync(int id);
    Task<Pago> UpdateAsync(Pago pago);
}
