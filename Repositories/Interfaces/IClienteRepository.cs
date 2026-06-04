using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<Cliente?> GetByUsuarioIdAsync(int idUsuario);
    Task<Cliente?> GetByEmailAsync(string email);
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<Cliente> UpdateAsync(Cliente cliente);
    Task SoftDeleteAsync(int id);
    Task<List<Models.Mascota>> GetMascotasAsync(int idCliente);
}
