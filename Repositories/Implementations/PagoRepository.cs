// ═══════════════════════════════════════════════════════
// ARCHIVO: PagoRepository.cs
// QUÉ HACE: El "cajero" que ejecuta consultas sobre la tabla PAGO.
//           No tiene CreateAsync porque los pagos los crea automáticamente
//           el SP sp_Cita_Create al agendar una cita — esta tabla nunca se
//           escribe directamente desde la app en la creación.
//           GetByCitaIdAsync es el método más usado: para ver cuánto debe el cliente
//           o para marcar un pago como realizado.
// QUIÉN LO USA: PagoService (inyectado vía DI como IPagoRepository)
// ═══════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using System.Linq;

namespace CuatroPatas.API.Repositories.Implementations;

public class PagoRepository : IPagoRepository
{
    private readonly AppDbContext _context;

    public PagoRepository(AppDbContext context) => _context = context;

    public async Task<Pago?> GetByCitaIdAsync(int idCita) =>
        await _context.Pagos.FirstOrDefaultAsync(p => p.IdCita == idCita);

    public async Task<Pago?> GetByIdAsync(int id) =>
        await _context.Pagos.FindAsync(id);

    public async Task<List<(Pago Pago, string NombreCliente)>> GetByClienteNombreAsync(string nombre)
    {
        var termino = nombre.ToLower().Trim();
        var resultados = await _context.Pagos
            .Join(_context.Citas, p => p.IdCita, c => c.IdCita, (p, c) => new { Pago = p, Cita = c })
            .Join(_context.Mascotas, x => x.Cita.IdMascota, m => m.IdMascota, (x, m) => new { x.Pago, x.Cita, Mascota = m })
            .Join(_context.Clientes, x => x.Mascota.IdCliente, cl => cl.IdCliente, (x, cl) => new { x.Pago, Cliente = cl })
            .Where(x => (x.Cliente.Nombre + " " + x.Cliente.Apellido).ToLower().Contains(termino)
                     || x.Cliente.Nombre.ToLower().Contains(termino)
                     || x.Cliente.Apellido.ToLower().Contains(termino))
            .Select(x => new { x.Pago, NombreCliente = x.Cliente.Nombre + " " + x.Cliente.Apellido })
            .ToListAsync();

        return resultados.Select(r => (r.Pago, r.NombreCliente)).ToList();
    }

    public async Task<Pago> UpdateAsync(Pago pago)
    {
        _context.Pagos.Update(pago);
        await _context.SaveChangesAsync();
        return pago;
    }
}
