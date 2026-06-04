using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IHistorialMedicoRepository
{
    Task<HistorialMedico?> GetByIdAsync(int id);
    Task<HistorialMedico> UpdateAsync(HistorialMedico historial);
}
