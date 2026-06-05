// ═══════════════════════════════════════════════════════
// ARCHIVO: AuthSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Usuario_Authenticate.
//           Cuando el usuario intenta hacer login, el SP devuelve una sola fila
//           con todos los datos necesarios: hash de contraseña, roles, estado de bloqueo.
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: AuthRepository.AutenticarUsuarioAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ IMPORTANTE: Las propiedades DEBEN ser snake_case para que EF las mapee
// correctamente al usar FromSqlRaw — EF compara por nombre de columna tal como
// los devuelve el SP, sin mayúsculas ni PascalCase.
public class AuthSpResult
{
    public int id_usuario { get; set; }
    public string? nombre_usuario { get; set; }
    public string? email { get; set; }
    // La "huella digital" de la contraseña — AuthService la compara con BCrypt.Verify()
    public string? password_hash { get; set; }
    // Los roles llegan concatenados como una sola cadena separada por coma: "Admin,Veterinario"
    // AuthService hace roles.Split(',') para convertirlos en lista
    public string? roles { get; set; }
    public bool activo { get; set; }
    // Si es true, el usuario no puede entrar aunque tenga la contraseña correcta
    public bool bloqueado { get; set; }
    // Se incrementa con cada intento fallido; al llegar al límite activa bloqueado=true
    public int intentos_fallidos { get; set; }
}
