using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.DTOs.Auth;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Models;
using CuatroPatas.API.Models.SpResults;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IUsuarioRepository _usuarioRepo;
    private readonly IClienteRepository _clienteRepo;
    private readonly IRefreshTokenRepository _refreshTokenRepo;
    private readonly JwtHelper _jwtHelper;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AppDbContext context,
        IUsuarioRepository usuarioRepo,
        IClienteRepository clienteRepo,
        IRefreshTokenRepository refreshTokenRepo,
        JwtHelper jwtHelper,
        ILogger<AuthService> logger)
    {
        _context = context;
        _usuarioRepo = usuarioRepo;
        _clienteRepo = clienteRepo;
        _refreshTokenRepo = refreshTokenRepo;
        _jwtHelper = jwtHelper;
        _logger = logger;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var results = await _context.Set<AuthSpResult>()
            .FromSqlRaw("EXEC sp_Usuario_Authenticate @email",
                new SqlParameter("@email", request.Email))
            .ToListAsync();

        var user = results.FirstOrDefault()
            ?? throw new UnauthorizedException("Credenciales inválidas.");

        if (!user.activo)
            throw new UnauthorizedException("Usuario inactivo.");

        if (user.bloqueado)
            throw new UnauthorizedException("Usuario bloqueado. Contacte al administrador.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.password_hash))
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Usuario_RegisterFailedLogin @id_usuario",
                new SqlParameter("@id_usuario", user.id_usuario));
            throw new UnauthorizedException("Credenciales inválidas.");
        }

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Usuario_RegisterSuccessLogin @id_usuario",
            new SqlParameter("@id_usuario", user.id_usuario));

        var roles = user.roles?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? [];
        var accessToken = _jwtHelper.GenerarToken(user.id_usuario, user.email!, roles);
        var refreshToken = _jwtHelper.GenerarRefreshToken();

        await _refreshTokenRepo.CreateAsync(new RefreshToken
        {
            IdUsuario = user.id_usuario,
            Token = refreshToken,
            FechaExpiracion = DateTime.UtcNow.AddDays(_jwtHelper.GetRefreshTokenDays()),
            FechaCreacion = DateTime.UtcNow
        });

        _logger.LogInformation("Login exitoso para usuario {Email}", request.Email);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtHelper.GetExpirationMinutes()),
            IdUsuario = user.id_usuario,
            Email = user.email!,
            NombreUsuario = user.nombre_usuario ?? string.Empty,
            Roles = roles
        };
    }

    public async Task<LoginResponse> RegisterAsync(RegisterRequest request)
    {
        var emailExiste = await _usuarioRepo.GetByEmailAsync(request.Email);
        if (emailExiste != null)
            throw new ConflictException("El email ya está registrado.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);

        var usuario = new Usuario
        {
            NombreUsuario = $"{request.Nombre} {request.Apellido}",
            Email = request.Email,
            PasswordHash = passwordHash,
            Activo = true,
            FechaCreacion = DateTime.Now
        };
        await _usuarioRepo.CreateAsync(usuario);

        var cliente = new Cliente
        {
            IdUsuario = usuario.IdUsuario,
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Email = request.Email,
            Telefono = request.Telefono,
            Direccion = request.Direccion,
            FechaRegistro = DateTime.Now
        };
        await _clienteRepo.CreateAsync(cliente);

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Usuario_AssignRole @id_usuario, @nombre_rol",
            new SqlParameter("@id_usuario", usuario.IdUsuario),
            new SqlParameter("@nombre_rol", "Cliente"));

        _logger.LogInformation("Registro exitoso para {Email}", request.Email);

        return await LoginAsync(new LoginRequest { Email = request.Email, Password = request.Password });
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var rt = await _refreshTokenRepo.GetByTokenAsync(request.RefreshToken)
            ?? throw new UnauthorizedException("Refresh token inválido.");

        if (rt.Revocado)
            throw new UnauthorizedException("Refresh token revocado.");

        if (rt.FechaExpiracion < DateTime.UtcNow)
            throw new UnauthorizedException("Refresh token expirado.");

        var roles = await _context.UsuarioRoles
            .Where(ur => ur.IdUsuario == rt.IdUsuario)
            .Include(ur => ur.Rol)
            .Select(ur => ur.Rol.NombreRol)
            .ToArrayAsync();

        var usuario = rt.Usuario;
        var accessToken = _jwtHelper.GenerarToken(usuario.IdUsuario, usuario.Email, roles);
        var newRefreshToken = _jwtHelper.GenerarRefreshToken();

        await _refreshTokenRepo.RevokeAsync(request.RefreshToken);
        await _refreshTokenRepo.CreateAsync(new RefreshToken
        {
            IdUsuario = usuario.IdUsuario,
            Token = newRefreshToken,
            FechaExpiracion = DateTime.UtcNow.AddDays(_jwtHelper.GetRefreshTokenDays()),
            FechaCreacion = DateTime.UtcNow
        });

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtHelper.GetExpirationMinutes()),
            IdUsuario = usuario.IdUsuario,
            Email = usuario.Email,
            NombreUsuario = usuario.NombreUsuario,
            Roles = roles
        };
    }

    public async Task LogoutAsync(string refreshToken)
    {
        await _refreshTokenRepo.RevokeAsync(refreshToken);
        _logger.LogInformation("Logout para refresh token terminado en ...{End}",
            refreshToken.Length > 8 ? refreshToken[^8..] : refreshToken);
    }

    public Task<MeResponse> GetMeAsync(int idUsuario, string email, IEnumerable<string> roles)
    {
        return Task.FromResult(new MeResponse
        {
            IdUsuario = idUsuario,
            Email = email,
            NombreUsuario = email,
            Roles = roles.ToArray()
        });
    }
}
