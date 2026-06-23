using CuatroPatas.API.DTOs.Notificacion;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class NotificacionService : INotificacionService
{
    private readonly INotificacionRepository _repo;

    public NotificacionService(INotificacionRepository repo) => _repo = repo;

    public async Task<List<NotificacionResponse>> GetByUsuarioAsync(int idUsuario)
    {
        var notifs = await _repo.GetByUsuarioAsync(idUsuario);
        return notifs.Select(n => new NotificacionResponse
        {
            IdNotificacion = n.IdNotificacion,
            IdUsuario = n.IdUsuario,
            IdCita = n.IdCita,
            Tipo = n.Tipo,
            Canal = n.Canal,
            Mensaje = n.Mensaje,
            Leida = n.Enviado,
            FechaProgramada = n.FechaProgramada,
            FechaLeida = n.FechaEnviado,
        }).ToList();
    }

    public async Task MarcarLeidaAsync(int id, int idUsuario)
    {
        var notifs = await _repo.GetByUsuarioAsync(idUsuario);
        if (!notifs.Any(n => n.IdNotificacion == id))
            throw new NotFoundException($"Notificacion {id} no encontrada para este usuario.");
        await _repo.MarkAsSentAsync(id);
    }

    public async Task MarcarTodasLeidasAsync(int idUsuario)
    {
        await _repo.MarkAllAsReadAsync(idUsuario);
    }
}
