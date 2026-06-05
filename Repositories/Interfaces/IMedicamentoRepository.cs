// ═══════════════════════════════════════════════════════
// ARCHIVO: IMedicamentoRepository.cs
// QUÉ HACE: Contrato del "cajero" que habla con la tabla MEDICAMENTO.
//           Gestiona el inventario de medicamentos y su nivel de stock.
// QUIÉN LO USA: MedicamentoRepository (implementación), MedicamentoService
// ═══════════════════════════════════════════════════════

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
