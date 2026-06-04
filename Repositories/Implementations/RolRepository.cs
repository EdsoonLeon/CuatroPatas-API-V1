using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Repositories.Implementations;

public class RolRepository : IRolRepository
{
    private readonly AppDbContext _context;

    public RolRepository(AppDbContext context) => _context = context;

    public async Task<List<Rol>> GetAllAsync() =>
        await _context.Roles.Where(r => r.Activo).ToListAsync();

    public async Task<Rol?> GetByIdAsync(int id) =>
        await _context.Roles.FindAsync(id);

    public async Task<Rol?> GetByNameAsync(string nombre) =>
        await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == nombre && r.Activo);

    public async Task<Rol> CreateAsync(Rol rol)
    {
        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();
        return rol;
    }

    public async Task<Rol> UpdateAsync(Rol rol)
    {
        _context.Roles.Update(rol);
        await _context.SaveChangesAsync();
        return rol;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var rol = await _context.Roles.FindAsync(id)
            ?? throw new NotFoundException($"Rol {id} no encontrado.");
        rol.Activo = false;
        await _context.SaveChangesAsync();
    }
}
