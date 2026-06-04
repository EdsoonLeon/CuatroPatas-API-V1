using CuatroPatas.API.DTOs.Prescripcion;

namespace CuatroPatas.API.Services.Interfaces;

public interface IPrescripcionService
{
    Task<PrescripcionResponse> CreateAsync(CreatePrescripcionRequest request);
    Task<List<PrescripcionResponse>> GetByHistorialAsync(int idHistorial);
    Task<List<PrescripcionResponse>> GetByMascotaAsync(int idMascota);
    Task<PrescripcionResponse> UpdateAsync(int id, UpdatePrescripcionRequest request);
    Task DeleteAsync(int id);
}
