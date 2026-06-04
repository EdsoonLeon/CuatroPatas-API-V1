using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IPrescripcionRepository
{
    Task<Prescripcion?> GetByIdAsync(int id);
    Task<Prescripcion> UpdateAsync(Prescripcion prescripcion);
    Task DeleteAsync(int id);
}
