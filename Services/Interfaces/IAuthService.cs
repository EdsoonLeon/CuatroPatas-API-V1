using CuatroPatas.API.DTOs.Auth;

namespace CuatroPatas.API.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> RegisterAsync(RegisterRequest request);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request);
    Task LogoutAsync(string refreshToken);
    Task<MeResponse> GetMeAsync(int idUsuario, string email, IEnumerable<string> roles);
}
