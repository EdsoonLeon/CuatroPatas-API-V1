// ═══════════════════════════════════════════════════════
// ARCHIVO: UsuarioRepository.cs
// QUÉ HACE: El "cajero" que ejecuta las consultas SQL sobre la tabla USUARIO.
//           Implementa IUsuarioRepository usando Entity Framework Core.
//           NOTA: GetByEmailAsync NO filtra por Activo=true — necesitamos
//           encontrar también usuarios desactivados para mostrar el mensaje correcto
//           en el login ("tu cuenta está desactivada"), en lugar de "usuario no encontrado".
// QUIÉN LO USA: AuthService, UsuarioService (inyectado vía DI como IUsuarioRepository)
// ═══════════════════════════════════════════════════════

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

    // Sin filtro Activo intencional — ver nota del archivo
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
        // Solo se actualiza el hash — no se toca ningún otro campo del usuario
        usuario.PasswordHash = newHash;
        await _context.SaveChangesAsync();
    }
}
