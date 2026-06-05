// ═══════════════════════════════════════════════════════
// ARCHIVO: MedicamentoRepository.cs
// QUÉ HACE: El "cajero" que ejecuta las consultas SQL sobre la tabla MEDICAMENTO.
//           CRUD básico — las operaciones de stock (descontar, reponer, stock bajo)
//           NO están aquí porque van por SPs con auditoría; las maneja MedicamentoService
//           directamente con ExecuteSqlRawAsync y FromSqlRaw.
// QUIÉN LO USA: MedicamentoService (inyectado vía DI como IMedicamentoRepository)
// ═══════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Repositories.Implementations;

public class MedicamentoRepository : IMedicamentoRepository
{
    private readonly AppDbContext _context;

    public MedicamentoRepository(AppDbContext context) => _context = context;

    public async Task<List<Medicamento>> GetAllAsync() =>
        await _context.Medicamentos.Where(m => m.Activo).ToListAsync();

    public async Task<Medicamento?> GetByIdAsync(int id) =>
        await _context.Medicamentos.FirstOrDefaultAsync(m => m.IdMedicamento == id && m.Activo);

    public async Task<Medicamento> CreateAsync(Medicamento medicamento)
    {
        _context.Medicamentos.Add(medicamento);
        await _context.SaveChangesAsync();
        return medicamento;
    }

    public async Task<Medicamento> UpdateAsync(Medicamento medicamento)
    {
        _context.Medicamentos.Update(medicamento);
        await _context.SaveChangesAsync();
        return medicamento;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var med = await _context.Medicamentos.FindAsync(id)
            ?? throw new NotFoundException($"Medicamento {id} no encontrado.");
        med.Activo = false;
        await _context.SaveChangesAsync();
    }
}
