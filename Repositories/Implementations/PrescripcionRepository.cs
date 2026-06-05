// ═══════════════════════════════════════════════════════
// ARCHIVO: PrescripcionRepository.cs
// QUÉ HACE: El "cajero" que ejecuta consultas simples sobre la tabla PRESCRIPCION.
//           DeleteAsync es la única excepción al patrón de borrado lógico del sistema:
//           aquí se elimina el registro físicamente porque los errores en recetas
//           médicas deben poder corregirse limpiamente, sin dejar rastros confusos.
//           La creación va por SP (en el servicio) para descontar stock atómicamente.
// QUIÉN LO USA: PrescripcionService (inyectado vía DI como IPrescripcionRepository)
// ═══════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Repositories.Implementations;

public class PrescripcionRepository : IPrescripcionRepository
{
    private readonly AppDbContext _context;

    public PrescripcionRepository(AppDbContext context) => _context = context;

    public async Task<Prescripcion?> GetByIdAsync(int id) =>
        await _context.Prescripciones.FindAsync(id);

    public async Task<Prescripcion> UpdateAsync(Prescripcion prescripcion)
    {
        _context.Prescripciones.Update(prescripcion);
        await _context.SaveChangesAsync();
        return prescripcion;
    }

    public async Task DeleteAsync(int id)
    {
        var p = await _context.Prescripciones.FindAsync(id)
            ?? throw new NotFoundException($"Prescripcion {id} no encontrada.");
        _context.Prescripciones.Remove(p);
        await _context.SaveChangesAsync();
    }
}
