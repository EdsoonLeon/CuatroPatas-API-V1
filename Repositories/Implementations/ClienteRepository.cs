// ═══════════════════════════════════════════════════════
// ARCHIVO: ClienteRepository.cs
// QUÉ HACE: El "cajero" que ejecuta las consultas SQL sobre la tabla CLIENTE.
//           Todas las lecturas filtran por Activo=true (borrado lógico).
//           GetMascotasAsync es un atajo conveniente que evita ir al MascotaRepository
//           cuando solo necesitas las mascotas de un cliente específico.
// QUIÉN LO USA: ClienteService (inyectado vía DI como IClienteRepository)
// ═══════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Helpers;

namespace CuatroPatas.API.Repositories.Implementations;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context) => _context = context;

    public async Task<List<Cliente>> GetAllAsync() =>
        await _context.Clientes.Where(c => c.Activo).ToListAsync();

    public async Task<Cliente?> GetByIdAsync(int id) =>
        await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id && c.Activo);

    public async Task<Cliente?> GetByUsuarioIdAsync(int idUsuario) =>
        await _context.Clientes.FirstOrDefaultAsync(c => c.IdUsuario == idUsuario && c.Activo);

    // Sin filtro Activo — permite detectar emails duplicados aunque el cliente esté desactivado
    public async Task<Cliente?> GetByEmailAsync(string email) =>
        await _context.Clientes.FirstOrDefaultAsync(c => c.Email == email);

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> UpdateAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id)
            ?? throw new NotFoundException($"Cliente {id} no encontrado.");
        cliente.Activo = false;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Mascota>> GetMascotasAsync(int idCliente) =>
        await _context.Mascotas.Where(m => m.IdCliente == idCliente && m.Activo).ToListAsync();
}
