namespace CuatroPatas.API.Models.SpResults;

public class AuthSpResult
{
    public int id_usuario { get; set; }
    public string? nombre_usuario { get; set; }
    public string? email { get; set; }
    public string? password_hash { get; set; }
    public string? roles { get; set; }
    public bool activo { get; set; }
    public bool bloqueado { get; set; }
    public int intentos_fallidos { get; set; }
}
