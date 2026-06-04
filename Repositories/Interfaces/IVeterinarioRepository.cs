using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IVeterinarioRepository
{
    Task<List<Veterinario>> GetAllAsync();
    Task<Veterinario?> GetByIdAsync(int id);
    Task<Veterinario?> GetByEmailAsync(string email);
    Task<Veterinario> CreateAsync(Veterinario veterinario);
    Task<Veterinario> UpdateAsync(Veterinario veterinario);
    Task SoftDeleteAsync(int id);
}
