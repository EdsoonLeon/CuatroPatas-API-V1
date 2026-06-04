using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IServicioRepository
{
    Task<List<Servicio>> GetAllAsync();
    Task<Servicio?> GetByIdAsync(int id);
    Task<Servicio> CreateAsync(Servicio servicio);
    Task<Servicio> UpdateAsync(Servicio servicio);
    Task SoftDeleteAsync(int id);
}
