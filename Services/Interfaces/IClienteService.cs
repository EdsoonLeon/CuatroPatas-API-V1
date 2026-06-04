using CuatroPatas.API.DTOs.Cliente;
using CuatroPatas.API.DTOs.Mascota;

namespace CuatroPatas.API.Services.Interfaces;

public interface IClienteService
{
    Task<List<ClienteResponse>> GetAllAsync();
    Task<ClienteResponse> GetByIdAsync(int id);
    Task<ClienteResponse> CreateAsync(CreateClienteRequest request);
    Task<ClienteResponse> UpdateAsync(int id, UpdateClienteRequest request);
    Task SoftDeleteAsync(int id);
    Task<List<MascotaResponse>> GetMascotasAsync(int idCliente);
}
