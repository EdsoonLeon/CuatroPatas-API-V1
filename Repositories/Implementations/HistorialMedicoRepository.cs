// ═══════════════════════════════════════════════════════
// ARCHIVO: HistorialMedicoRepository.cs
// QUÉ HACE: El "cajero" que ejecuta consultas simples sobre HISTORIAL_MEDICO.
//           Interfaz deliberadamente pequeña: la creación y los listados van por SPs
//           (con datos enriquecidos de múltiples tablas), así que solo exponemos
//           GetById (para verificar existencia) y Update (para ediciones directas).
// QUIÉN LO USA: HistorialMedicoService (inyectado vía DI como IHistorialMedicoRepository)
// ═══════════════════════════════════════════════════════

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
