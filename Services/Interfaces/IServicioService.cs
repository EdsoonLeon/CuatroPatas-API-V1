// ═══════════════════════════════════════════════════════
// ARCHIVO: IServicioService.cs
// QUÉ HACE: Contrato del servicio del catálogo de servicios clínicos
//           (consulta, vacunación, cirugía, baño, etc.).
//           CRUD simple — no hay lógica de negocio compleja en este módulo.
// QUIÉN LO USA: ServicioController (inyectado como IServicioService)
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.DTOs.Servicio;

namespace CuatroPatas.API.Services.Interfaces;

public interface IServicioService
{
    Task<List<ServicioResponse>> GetAllAsync();
    Task<ServicioResponse> CreateAsync(CreateServicioRequest request);
    Task<ServicioResponse> UpdateAsync(int id, UpdateServicioRequest request);
    Task SoftDeleteAsync(int id);
}
