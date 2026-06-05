// ═══════════════════════════════════════════════════════
// ARCHIVO: IPrescripcionService.cs
// QUÉ HACE: Contrato del servicio de prescripciones (recetas médicas).
//           CreateAsync va por SP porque la creación descuenta stock automáticamente.
//           DeleteAsync es eliminación física — excepción al patrón de soft delete
//           para que los errores en recetas se puedan corregir limpiamente.
// QUIÉN LO USA: PrescripcionController (inyectado como IPrescripcionService)
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.DTOs.Prescripcion;

namespace CuatroPatas.API.Services.Interfaces;

public interface IPrescripcionService
{
    Task<PrescripcionResponse> CreateAsync(CreatePrescripcionRequest request);
    Task<List<PrescripcionResponse>> GetByHistorialAsync(int idHistorial);
    Task<List<PrescripcionResponse>> GetByMascotaAsync(int idMascota);
    Task<PrescripcionResponse> UpdateAsync(int id, UpdatePrescripcionRequest request);
    Task DeleteAsync(int id);
}
