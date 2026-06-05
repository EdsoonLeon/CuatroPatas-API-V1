// ═══════════════════════════════════════════════════════
// ARCHIVO: IVeterinarioRepository.cs
// QUÉ HACE: Contrato del "cajero" que habla con la tabla VETERINARIO.
//           GetByEmailAsync permite validar unicidad de email antes de crear
//           o detectar al vet por su correo al hacer login.
// QUIÉN LO USA: VeterinarioRepository (implementación), VeterinarioService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IVeterinarioRepository
{
    Task<List<Veterinario>> GetAllAsync();
    Task<Veterinario?> GetByIdAsync(int id);
    Task<Veterinario?> GetByEmailAsync(string email);
    Task<Veterinario> CreateAsync(Veterinario veterinario);
    Task<Veterinario> UpdateAsync(Veterinario veterinario);
    Task SoftDeleteAsync(int id);
}
