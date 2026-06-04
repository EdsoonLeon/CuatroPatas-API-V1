using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Repositories.Implementations;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context) => _context = context;

    public async Task<Usuario?> GetByIdAsync(int id) =>
        await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && u.Activo);

    public async Task<Usuario?> GetByEmailAsync(string email) =>
        await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id)
            ?? throw new NotFoundException($"Usuario {id} no encontrado.");
        usuario.Activo = false;
        await _context.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(int id, string newHash)
    {
        var usuario = await _context.Usuarios.FindAsync(id)
            ?? throw new NotFoundException($"Usuario {id} no encontrado.");
        usuario.PasswordHash = newHash;
        await _context.SaveChangesAsync();
    }
}
