using CuatroPatas.API.DTOs.Notificacion;

namespace CuatroPatas.API.Services.Interfaces;

public interface INotificacionService
{
    Task<List<NotificacionResponse>> GetByUsuarioAsync(int idUsuario);
    Task MarcarLeidaAsync(int id, int idUsuario);
    Task MarcarTodasLeidasAsync(int idUsuario);
}
