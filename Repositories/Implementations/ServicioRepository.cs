using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Repositories.Implementations;

public class ServicioRepository : IServicioRepository
{
    private readonly AppDbContext _context;

    public ServicioRepository(AppDbContext context) => _context = context;

    public async Task<List<Servicio>> GetAllAsync() =>
        await _context.Servicios.Where(s => s.Activo).ToListAsync();

    public async Task<Servicio?> GetByIdAsync(int id) =>
        await _context.Servicios.FirstOrDefaultAsync(s => s.IdServicio == id && s.Activo);

    public async Task<Servicio> CreateAsync(Servicio servicio)
    {
        _context.Servicios.Add(servicio);
        await _context.SaveChangesAsync();
        return servicio;
    }

    public async Task<Servicio> UpdateAsync(Servicio servicio)
    {
        _context.Servicios.Update(servicio);
        await _context.SaveChangesAsync();
        return servicio;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var servicio = await _context.Servicios.FindAsync(id)
            ?? throw new NotFoundException($"Servicio {id} no encontrado.");
        servicio.Activo = false;
        await _context.SaveChangesAsync();
    }
}
