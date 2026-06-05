// ═══════════════════════════════════════════════════════
// ARCHIVO: IPrescripcionRepository.cs
// QUÉ HACE: Contrato del "cajero" que habla con la tabla PRESCRIPCION.
//           DeleteAsync es eliminación física (no lógica) — excepción al patrón del sistema.
//           Las prescripciones se pueden eliminar porque si hay un error en la receta,
//           el médico necesita borrarla y reemitirla correctamente.
// QUIÉN LO USA: PrescripcionRepository (implementación), PrescripcionService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IPrescripcionRepository
{
    Task<Prescripcion?> GetByIdAsync(int id);
    Task<Prescripcion> UpdateAsync(Prescripcion prescripcion);
    // Eliminación física — excepción justificada: errores en recetas se deben poder corregir limpiamente
    Task DeleteAsync(int id);
}
