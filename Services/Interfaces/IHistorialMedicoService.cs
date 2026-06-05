// ═══════════════════════════════════════════════════════
// ARCHIVO: IHistorialMedicoService.cs
// QUÉ HACE: Contrato del servicio del historial médico (expediente clínico).
//           GetByMascotaAsync filtra por tipo y rango de fechas para búsquedas específicas.
//           GetVacunasAsync es un atajo directo al historial de vacunación,
//           que es la consulta más frecuente en las revisiones preventivas.
// QUIÉN LO USA: HistorialController (inyectado como IHistorialMedicoService)
// ═══════════════════════════════════════════════════════

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
