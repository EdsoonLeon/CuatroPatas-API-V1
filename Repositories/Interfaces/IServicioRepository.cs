// ═══════════════════════════════════════════════════════
// ARCHIVO: IServicioRepository.cs
// QUÉ HACE: Contrato del "cajero" que habla con la tabla SERVICIO.
//           Gestiona el catálogo de servicios: consulta, vacuna, cirugía, baño, etc.
// QUIÉN LO USA: ServicioRepository (implementación), ServicioService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IServicioRepository
{
    Task<List<Servicio>> GetAllAsync();
    Task<Servicio?> GetByIdAsync(int id);
    Task<Servicio> CreateAsync(Servicio servicio);
    Task<Servicio> UpdateAsync(Servicio servicio);
    // Borrado lógico — pone Activo = false para no afectar DetalleCita histórico
    Task SoftDeleteAsync(int id);
}
