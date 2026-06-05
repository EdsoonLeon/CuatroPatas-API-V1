// ═══════════════════════════════════════════════════════
// ARCHIVO: IClienteService.cs
// QUÉ HACE: Contrato del servicio de clientes (dueños de mascotas).
//           Incluye GetMascotasAsync como atajo conveniente para el flujo
//           más común: ver qué mascotas tiene un cliente específico.
// QUIÉN LO USA: ClienteController (inyectado como IClienteService)
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.DTOs.Cliente;
using CuatroPatas.API.DTOs.Mascota;

namespace CuatroPatas.API.Services.Interfaces;

public interface IClienteService
{
    Task<List<ClienteResponse>> GetAllAsync();
    Task<ClienteResponse> GetByIdAsync(int id);
    Task<ClienteResponse> CreateAsync(CreateClienteRequest request);
    Task<ClienteResponse> UpdateAsync(int id, UpdateClienteRequest request);
    Task SoftDeleteAsync(int id);
    Task<List<MascotaResponse>> GetMascotasAsync(int idCliente);
}
