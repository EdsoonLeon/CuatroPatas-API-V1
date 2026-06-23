namespace CuatroPatas.API.DTOs.Notificacion;

public class NotificacionResponse
{
    public int IdNotificacion { get; set; }
    public int IdUsuario { get; set; }
    public int? IdCita { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Canal { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public bool Leida { get; set; }
    public DateTime FechaProgramada { get; set; }
    public DateTime? FechaLeida { get; set; }
}
