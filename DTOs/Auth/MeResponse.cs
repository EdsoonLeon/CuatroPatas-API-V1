namespace CuatroPatas.API.DTOs.Auth;

public class MeResponse
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string[] Roles { get; set; } = [];
}
