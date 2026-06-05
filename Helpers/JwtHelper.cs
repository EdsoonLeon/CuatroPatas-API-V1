// ═══════════════════════════════════════════════════════
// ARCHIVO: JwtHelper.cs
// QUÉ HACE: Crea y gestiona los tokens de autenticación del sistema.
//           Genera el JWT (el "carnet digital" del usuario) y el Refresh Token
//           (el "código de renovación" cuando el carnet expira).
// QUIÉN LO USA: AuthService al hacer login, registro y renovación de token
// ═══════════════════════════════════════════════════════

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CuatroPatas.API.Helpers;

public class JwtHelper
{
    private readonly IConfiguration _config;

    public JwtHelper(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Crea el "carnet digital" del usuario (token JWT).
    /// Este carnet lleva dentro el ID del usuario, su email y sus roles (permisos).
    /// Tiene una fecha de expiración (normalmente 60 minutos).
    /// El usuario lo presenta en cada petición para demostrar quién es.
    /// </summary>
    /// <param name="idUsuario">El ID numérico del usuario en la base de datos</param>
    /// <param name="email">El email del usuario, que va dentro del token</param>
    /// <param name="roles">Los roles del usuario (ej: "Administrador", "Veterinario")</param>
    /// <returns>El token JWT como string — una cadena larga que empieza con "eyJ..."</returns>
    public string GenerarToken(int idUsuario, string email, IEnumerable<string> roles)
    {
        var secretKey = _config["JwtSettings:SecretKey"]!;
        var issuer = _config["JwtSettings:Issuer"]!;
        var audience = _config["JwtSettings:Audience"]!;
        var expirationMinutes = int.Parse(_config["JwtSettings:ExpirationMinutes"]!);

        // Creamos la llave de firma usando la clave secreta del appsettings.json
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Los "claims" son la información que viaja DENTRO del token
        // Es como los datos impresos en el carnet de identidad
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, idUsuario.ToString()), // El ID del usuario
            new(ClaimTypes.Email, email),                          // Su email
            new(JwtRegisteredClaimNames.Sub, idUsuario.ToString()), // "Sub" = sujeto del token (estándar JWT)
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // ID único de este token específico
        };

        // Agregamos un claim separado por cada rol que tiene el usuario.
        // Necesitamos uno por rol (no todos juntos en uno) para que ASP.NET
        // pueda verificar políticas como [Authorize(Policy="SoloAdmin")] correctamente.
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Genera el "código de renovación" (Refresh Token).
    /// Este token no tiene información del usuario dentro — solo es una clave aleatoria
    /// que guardamos en la base de datos. Cuando el JWT expira, el frontend envía
    /// este código para obtener un JWT nuevo sin pedir la contraseña de nuevo.
    /// Dura 7 días antes de expirar (configurable en appsettings.json).
    /// </summary>
    /// <returns>Una cadena aleatoria de 88 caracteres en Base64, criptográficamente segura</returns>
    public string GenerarRefreshToken()
    {
        var randomBytes = new byte[64];
        // RandomNumberGenerator es la fuente de aleatoriedad más segura disponible en .NET
        // No usamos Random.Next() porque podría ser predecible
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    /// <summary>Devuelve los días de vida del Refresh Token (del appsettings.json)</summary>
    public int GetRefreshTokenDays() =>
        int.Parse(_config["JwtSettings:RefreshTokenDays"]!);

    /// <summary>Devuelve los minutos de vida del JWT (del appsettings.json)</summary>
    public int GetExpirationMinutes() =>
        int.Parse(_config["JwtSettings:ExpirationMinutes"]!);
}
