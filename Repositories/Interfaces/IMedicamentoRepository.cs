using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IMedicamentoRepository
{
    Task<List<Medicamento>> GetAllAsync();
    Task<Medicamento?> GetByIdAsync(int id);
    Task<Medicamento> CreateAsync(Medicamento medicamento);
    Task<Medicamento> UpdateAsync(Medicamento medicamento);
    Task SoftDeleteAsync(int id);
}
