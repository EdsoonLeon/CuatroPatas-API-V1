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

    public string GenerarToken(int idUsuario, string email, IEnumerable<string> roles)
    {
        var secretKey = _config["JwtSettings:SecretKey"]!;
        var issuer = _config["JwtSettings:Issuer"]!;
        var audience = _config["JwtSettings:Audience"]!;
        var expirationMinutes = int.Parse(_config["JwtSettings:ExpirationMinutes"]!);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, idUsuario.ToString()),
            new(ClaimTypes.Email, email),
            new(JwtRegisteredClaimNames.Sub, idUsuario.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

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

    public string GenerarRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public int GetRefreshTokenDays() =>
        int.Parse(_config["JwtSettings:RefreshTokenDays"]!);

    public int GetExpirationMinutes() =>
        int.Parse(_config["JwtSettings:ExpirationMinutes"]!);
}
