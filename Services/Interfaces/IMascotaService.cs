// ═══════════════════════════════════════════════════════
// ARCHIVO: IMascotaService.cs
// QUÉ HACE: Contrato del servicio de mascotas.
//           Agrupa el CRUD de mascotas junto con GetHistorialAsync y
//           GetProximasCitasAsync — las dos consultas más usadas desde
//           el perfil de una mascota en el portal del cliente.
// QUIÉN LO USA: MascotaController (inyectado como IMascotaService)
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.DTOs.Mascota;
using CuatroPatas.API.DTOs.Historial;
using CuatroPatas.API.DTOs.Cita;

namespace CuatroPatas.API.Services.Interfaces;

public interface IMascotaService
{
    Task<List<MascotaResponse>> GetAllAsync();
    Task<MascotaResponse> GetByIdAsync(int id);
    Task<MascotaResponse> CreateAsync(CreateMascotaRequest request);
    Task<MascotaResponse> UpdateAsync(int id, UpdateMascotaRequest request);
    Task SoftDeleteAsync(int id);
    Task<List<HistorialResponse>> GetHistorialAsync(int idMascota);
    Task<List<CitaResponse>> GetProximasCitasAsync(int idMascota);
}
