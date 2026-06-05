// ═══════════════════════════════════════════════════════
// ARCHIVO: ICitaService.cs
// QUÉ HACE: Contrato del servicio de citas.
//           Define las operaciones del ciclo de vida de una cita:
//           crear, consultar, cambiar estado, cancelar y gestionar sus servicios.
// QUIÉN LO USA: CitaController (inyectado como ICitaService)
// ═══════════════════════════════════════════════════════

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
