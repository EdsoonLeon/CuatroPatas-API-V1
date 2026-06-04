using CuatroPatas.API.DTOs.Veterinario;

namespace CuatroPatas.API.Services.Interfaces;

public interface IVeterinarioService
{
    Task<List<VeterinarioResponse>> GetAllAsync();
    Task<VeterinarioResponse> GetByIdAsync(int id);
    Task<VeterinarioResponse> CreateAsync(CreateVeterinarioRequest request);
    Task<VeterinarioResponse> UpdateAsync(int id, UpdateVeterinarioRequest request);
    Task SoftDeleteAsync(int id);
    Task<List<AgendaResponse>> GetAgendaAsync(int idVeterinario, DateTime fechaInicio, DateTime fechaFin);
    Task<EstadisticasResponse> GetEstadisticasAsync(int idVeterinario, DateTime? fechaInicio, DateTime? fechaFin);
}
