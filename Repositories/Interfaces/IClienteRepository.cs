// ═══════════════════════════════════════════════════════
// ARCHIVO: IClienteRepository.cs
// QUÉ HACE: Contrato del "cajero" que habla con la tabla CLIENTE.
//           GetByUsuarioIdAsync permite encontrar el perfil de cliente a partir
//           del ID del usuario autenticado — útil cuando el cliente solo ve sus propios datos.
//           GetMascotasAsync evita ir al MascotaRepository para una consulta común.
// QUIÉN LO USA: ClienteRepository (implementación), ClienteService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    // Vincula la sesión del usuario con su perfil de cliente
    Task<Cliente?> GetByUsuarioIdAsync(int idUsuario);
    Task<Cliente?> GetByEmailAsync(string email);
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<Cliente> UpdateAsync(Cliente cliente);
    Task SoftDeleteAsync(int id);
    // Devuelve todas las mascotas registradas a nombre de este cliente
    Task<List<Models.Mascota>> GetMascotasAsync(int idCliente);
}
