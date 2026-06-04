using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;

namespace CuatroPatas.API.Repositories.Implementations;

public class HistorialMedicoRepository : IHistorialMedicoRepository
{
    private readonly AppDbContext _context;

    public HistorialMedicoRepository(AppDbContext context) => _context = context;

    public async Task<HistorialMedico?> GetByIdAsync(int id) =>
        await _context.HistorialesMedicos
            .FirstOrDefaultAsync(h => h.IdHistorial == id && h.Activo);

    public async Task<HistorialMedico> UpdateAsync(HistorialMedico historial)
    {
        _context.HistorialesMedicos.Update(historial);
        await _context.SaveChangesAsync();
        return historial;
    }
}
