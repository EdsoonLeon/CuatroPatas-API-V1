using CuatroPatas.API.DTOs.Mascota;
using CuatroPatas.API.DTOs.Historial;
using CuatroPatas.API.DTOs.Cita;

namespace CuatroPatas.API.Services.Interfaces;

public interface IMascotaService
{
    Task<List<MascotaResponse>> GetAllAsync();
    Task<MascotaResponse> GetByIdAsync(int id);
    Task<MascotaResponse> CreateAsync(CreateMascotaRequest request);
    Task<MascotaResponse> UpdateAsync(int id, UpdateMascotaRequest request);
    Task SoftDeleteAsync(int id);
    Task<List<HistorialResponse>> GetHistorialAsync(int idMascota);
    Task<List<CitaResponse>> GetProximasCitasAsync(int idMascota);
}
