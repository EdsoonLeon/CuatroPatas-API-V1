// ═══════════════════════════════════════════════════════
// ARCHIVO: IHistorialMedicoRepository.cs
// QUÉ HACE: Contrato del "cajero" que habla con la tabla HISTORIAL_MEDICO.
//           Las operaciones de lectura y creación van por SPs (en el servicio directamente
//           con FromSqlRaw), por eso la interfaz solo expone GetById y Update.
// QUIÉN LO USA: HistorialMedicoRepository (implementación), HistorialService
// ═══════════════════════════════════════════════════════

using CuatroPatas.API.Models;

namespace CuatroPatas.API.Repositories.Interfaces;

public interface IHistorialMedicoRepository
{
    Task<HistorialMedico?> GetByIdAsync(int id);
    Task<HistorialMedico> UpdateAsync(HistorialMedico historial);
}
