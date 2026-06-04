using CuatroPatas.API.DTOs.Medicamento;

namespace CuatroPatas.API.Services.Interfaces;

public interface IMedicamentoService
{
    Task<List<MedicamentoResponse>> GetAllAsync();
    Task<MedicamentoResponse> GetByIdAsync(int id);
    Task<MedicamentoResponse> CreateAsync(CreateMedicamentoRequest request);
    Task<MedicamentoResponse> UpdateAsync(int id, UpdateMedicamentoRequest request);
    Task SoftDeleteAsync(int id);
    Task<List<MedicamentoResponse>> GetStockBajoAsync();
    Task DescontarStockAsync(int id, int cantidad);
    Task ReponerStockAsync(int id, int cantidad);
}
