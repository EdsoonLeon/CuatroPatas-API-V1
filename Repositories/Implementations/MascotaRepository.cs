// ═══════════════════════════════════════════════════════
// ARCHIVO: MascotaRepository.cs
// QUÉ HACE: El "cajero" que ejecuta las consultas SQL sobre la tabla MASCOTA.
//           Detalle importante: CreateAsync y UpdateAsync llaman a ReloadAsync()
//           después de guardar para traer el campo edad_calculada, que SQL Server
//           calcula automáticamente — EF no lo tiene en memoria hasta ese refresh.
// QUIÉN LO USA: MascotaService (inyectado vía DI como IMascotaRepository)
// ═══════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Repositories.Implementations;

public class MascotaRepository : IMascotaRepository
{
    private readonly AppDbContext _context;

    public MascotaRepository(AppDbContext context) => _context = context;

    public async Task<List<Mascota>> GetAllAsync() =>
        await _context.Mascotas.Where(m => m.Activo).ToListAsync();

    public async Task<Mascota?> GetByIdAsync(int id) =>
        await _context.Mascotas.FirstOrDefaultAsync(m => m.IdMascota == id && m.Activo);

    public async Task<List<Mascota>> GetByClienteIdAsync(int idCliente) =>
        await _context.Mascotas.Where(m => m.IdCliente == idCliente && m.Activo).ToListAsync();

    public async Task<Mascota> CreateAsync(Mascota mascota)
    {
        _context.Mascotas.Add(mascota);
        await _context.SaveChangesAsync();
        // ReloadAsync para traer EdadCalculada que SQL Server generó — EF no la tiene en el objeto en memoria
        await _context.Entry(mascota).ReloadAsync();
        return mascota;
    }

    public async Task<Mascota> UpdateAsync(Mascota mascota)
    {
        _context.Mascotas.Update(mascota);
        await _context.SaveChangesAsync();
        // Mismo motivo: la columna computed puede cambiar si se modificó FechaNacimiento
        await _context.Entry(mascota).ReloadAsync();
        return mascota;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var mascota = await _context.Mascotas.FindAsync(id)
            ?? throw new NotFoundException($"Mascota {id} no encontrada.");
        mascota.Activo = false;
        await _context.SaveChangesAsync();
    }
}
