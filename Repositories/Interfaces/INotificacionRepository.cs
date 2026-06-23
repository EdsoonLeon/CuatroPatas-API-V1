// ═══════════════════════════════════════════════════════
// ARCHIVO: INotificacionRepository.cs
// QUÉ HACE: Contrato del "cajero" que maneja la tabla NOTIFICACION.
//           Interfaz mínima: solo crea notificaciones y las marca como enviadas.
//           La lectura de notificaciones del usuario se hace vía SP en el servicio.
// QUIÉN LO USA: NotificacionRepository (implementación), NotificacionService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface INotificacionRepository
{
    Task<Notificacion> CreateAsync(Notificacion notificacion);
    Task MarkAsSentAsync(int id);
    Task<List<Notificacion>> GetByUsuarioAsync(int idUsuario);
    Task MarkAllAsReadAsync(int idUsuario);
}
