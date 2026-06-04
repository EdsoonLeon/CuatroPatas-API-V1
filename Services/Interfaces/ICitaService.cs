using CuatroPatas.API.DTOs.Cita;

namespace CuatroPatas.API.Services.Interfaces;

public interface ICitaService
{
    Task<List<CitaResponse>> GetAllAsync(CitaFilterRequest filter);
    Task<List<CitaResponse>> GetTodayAsync(int? idVeterinario);
    Task<List<CitaResponse>> GetMisCitasAsync(int idUsuario);
    Task<CitaResponse> GetByIdAsync(int id);
    Task<CitaResponse> CreateAsync(CreateCitaRequest request, int idUsuario);
    Task ChangeEstadoAsync(int id, ChangeEstadoCitaRequest request, int idUsuario);
    Task CancelAsync(int id, CancelCitaRequest request, int idUsuario);
    Task AddServicioAsync(int id, AddServicioCitaRequest request);
    Task<List<CitaServicioResponse>> GetServiciosAsync(int id);
}
