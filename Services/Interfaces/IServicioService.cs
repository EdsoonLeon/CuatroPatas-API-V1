using CuatroPatas.API.DTOs.Servicio;

namespace CuatroPatas.API.Services.Interfaces;

public interface IServicioService
{
    Task<List<ServicioResponse>> GetAllAsync();
    Task<ServicioResponse> CreateAsync(CreateServicioRequest request);
    Task<ServicioResponse> UpdateAsync(int id, UpdateServicioRequest request);
    Task SoftDeleteAsync(int id);
}
