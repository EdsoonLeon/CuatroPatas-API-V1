// ═══════════════════════════════════════════════════════
// ARCHIVO: IMascotaRepository.cs
// QUÉ HACE: Contrato del "cajero" que habla con la tabla MASCOTA.
//           GetByClienteIdAsync permite listar todas las mascotas de un cliente —
//           es la consulta más frecuente cuando el cliente entra a su perfil.
// QUIÉN LO USA: MascotaRepository (implementación), MascotaService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IMascotaRepository
{
    Task<List<Mascota>> GetAllAsync();
    Task<Mascota?> GetByIdAsync(int id);
    // La consulta más frecuente — todas las mascotas de un dueño específico
    Task<List<Mascota>> GetByClienteIdAsync(int idCliente);
    Task<Mascota> CreateAsync(Mascota mascota);
    Task<Mascota> UpdateAsync(Mascota mascota);
    Task SoftDeleteAsync(int id);
}
