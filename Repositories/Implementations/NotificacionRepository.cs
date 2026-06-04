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
        n.Enviada = true;
        n.FechaEnvio = DateTime.Now;
        await _context.SaveChangesAsync();
    }
}
