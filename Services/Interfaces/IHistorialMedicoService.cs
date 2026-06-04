using CuatroPatas.API.DTOs.Historial;

namespace CuatroPatas.API.Services.Interfaces;

public interface IHistorialMedicoService
{
    Task<HistorialResponse> CreateAsync(CreateHistorialRequest request);
    Task<HistorialResponse> GetByIdAsync(int id);
    Task<List<HistorialResponse>> GetByMascotaAsync(int idMascota, string? tipoRegistro, DateTime? fechaInicio, DateTime? fechaFin);
    Task<List<HistorialResponse>> GetVacunasAsync(int idMascota);
    Task SoftDeleteAsync(int id);
}
