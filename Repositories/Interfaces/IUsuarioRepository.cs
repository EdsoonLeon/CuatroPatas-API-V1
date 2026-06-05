// ═══════════════════════════════════════════════════════
// ARCHIVO: IUsuarioRepository.cs
// QUÉ HACE: Contrato del "cajero" que habla con la tabla USUARIO.
//           Define qué operaciones de base de datos están disponibles para usuarios.
//           ChangePasswordAsync existe como método separado para que el hash BCrypt
//           nunca pase por AutoMapper ni por el modelo general — solo va directo a la BD.
// QUIÉN LO USA: UsuarioRepository (implementación), AuthService, UsuarioService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario> CreateAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    // Borrado lógico — pone Activo = false, no elimina el registro
    Task SoftDeleteAsync(int id);
    // Recibe el hash ya calculado con BCrypt — nunca la contraseña en texto plano
    Task ChangePasswordAsync(int id, string newHash);
}
