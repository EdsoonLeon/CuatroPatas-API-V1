using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IRolRepository
{
    Task<List<Rol>> GetAllAsync();
    Task<Rol?> GetByIdAsync(int id);
    Task<Rol?> GetByNameAsync(string nombre);
    Task<Rol> CreateAsync(Rol rol);
    Task<Rol> UpdateAsync(Rol rol);
    Task SoftDeleteAsync(int id);
}
