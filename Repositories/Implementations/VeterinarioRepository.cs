// ═══════════════════════════════════════════════════════
// ARCHIVO: VeterinarioRepository.cs
// QUÉ HACE: El "cajero" que ejecuta las consultas SQL sobre la tabla VETERINARIO.
//           Todas las lecturas filtran por Activo=true (borrado lógico).
//           GetByEmailAsync permite detectar duplicados antes de registrar un nuevo vet.
//           La agenda y las estadísticas NO están aquí — las maneja VeterinarioService
//           directamente con SPs porque requieren JOINs con otras tablas.
// QUIÉN LO USA: VeterinarioService (inyectado vía DI como IVeterinarioRepository)
// ═══════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Repositories.Implementations;

public class VeterinarioRepository : IVeterinarioRepository
{
    private readonly AppDbContext _context;

    public VeterinarioRepository(AppDbContext context) => _context = context;

    public async Task<List<Veterinario>> GetAllAsync() =>
        await _context.Veterinarios.Where(v => v.Activo).ToListAsync();

    public async Task<Veterinario?> GetByIdAsync(int id) =>
        await _context.Veterinarios.FirstOrDefaultAsync(v => v.IdVeterinario == id && v.Activo);

    public async Task<Veterinario?> GetByEmailAsync(string email) =>
        await _context.Veterinarios.FirstOrDefaultAsync(v => v.Email == email);

    public async Task<Veterinario> CreateAsync(Veterinario veterinario)
    {
        _context.Veterinarios.Add(veterinario);
        await _context.SaveChangesAsync();
        return veterinario;
    }

    public async Task<Veterinario> UpdateAsync(Veterinario veterinario)
    {
        _context.Veterinarios.Update(veterinario);
        await _context.SaveChangesAsync();
        return veterinario;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var vet = await _context.Veterinarios.FindAsync(id)
            ?? throw new NotFoundException($"Veterinario {id} no encontrado.");
        vet.Activo = false;
        await _context.SaveChangesAsync();
    }
}
