using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface INotificacionRepository
{
    Task<Notificacion> CreateAsync(Notificacion notificacion);
    Task MarkAsSentAsync(int id);
}
