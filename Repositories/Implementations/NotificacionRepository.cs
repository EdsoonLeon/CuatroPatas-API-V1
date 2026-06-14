// ═══════════════════════════════════════════════════════
// ARCHIVO: NotificacionRepository.cs
// QUÉ HACE: El "cajero" que escribe y actualiza notificaciones en la tabla NOTIFICACION.
//           CreateAsync guarda un nuevo aviso (recordatorio de cita, alerta de stock, etc.).
//           MarkAsSentAsync actualiza Enviada = true y FechaEnvio cuando el sistema
//           de despacho confirma que la notificación llegó al canal del usuario.
// QUIÉN LO USA: NotificacionService (inyectado vía DI como INotificacionRepository)
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Repositories.Implementations;

public class NotificacionRepository : INotificacionRepository
{
    private readonly AppDbContext _context;

    public NotificacionRepository(AppDbContext context) => _context = context;

    public async Task<Notificacion> CreateAsync(Notificacion notificacion)
    {
        _context.Notificaciones.Add(notificacion);
        await _context.SaveChangesAsync();
        return notificacion;
    }

    public async Task MarkAsSentAsync(int id)
    {
        var n = await _context.Notificaciones.FindAsync(id)
            ?? throw new NotFoundException($"Notificacion {id} no encontrada.");
        n.Enviado = true;
        n.FechaEnviado = DateTime.Now;
        await _context.SaveChangesAsync();
    }
}
