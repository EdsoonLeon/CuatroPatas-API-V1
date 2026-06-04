using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Auth;

public class LogoutRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
