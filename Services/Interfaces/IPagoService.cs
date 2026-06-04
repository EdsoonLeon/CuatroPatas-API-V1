using CuatroPatas.API.DTOs.Pago;

namespace CuatroPatas.API.Services.Interfaces;

public interface IPagoService
{
    Task<PagoResponse> GetByCitaAsync(int idCita);
    Task<PagoResponse> UpdateAsync(int id, UpdatePagoRequest request);
}
