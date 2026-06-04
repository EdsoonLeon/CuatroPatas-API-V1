using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IMascotaRepository
{
    Task<List<Mascota>> GetAllAsync();
    Task<Mascota?> GetByIdAsync(int id);
    Task<List<Mascota>> GetByClienteIdAsync(int idCliente);
    Task<Mascota> CreateAsync(Mascota mascota);
    Task<Mascota> UpdateAsync(Mascota mascota);
    Task SoftDeleteAsync(int id);
}
