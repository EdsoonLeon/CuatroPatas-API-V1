using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Auth;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
