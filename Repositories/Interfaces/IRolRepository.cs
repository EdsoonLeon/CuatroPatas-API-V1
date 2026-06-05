// ═══════════════════════════════════════════════════════
// ARCHIVO: IRolRepository.cs
// QUÉ HACE: Contrato del "cajero" que maneja la tabla ROL.
//           Los roles son: Admin, Veterinario, Cliente.
//           GetByNameAsync se usa para asignar el rol correcto al crear un usuario.
// QUIÉN LO USA: RolRepository (implementación), AuthService (al registrar)
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IRolRepository
{
    Task<List<Rol>> GetAllAsync();
    Task<Rol?> GetByIdAsync(int id);
    // Busca por nombre exacto: "Admin", "Veterinario", "Cliente"
    Task<Rol?> GetByNameAsync(string nombre);
    Task<Rol> CreateAsync(Rol rol);
    Task<Rol> UpdateAsync(Rol rol);
    Task SoftDeleteAsync(int id);
}
