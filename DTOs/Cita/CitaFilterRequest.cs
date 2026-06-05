// ═══════════════════════════════════════════════════════
// ARCHIVO: CitaFilterRequest.cs
// QUÉ HACE: Parámetros opcionales para filtrar la lista de citas.
//           Se pasan al SP sp_Cita_List. Si un filtro es null, el SP lo ignora
//           (NULL se convierte a DBNull.Value en el repositorio) y devuelve todos los registros.
//           Permite que un mismo endpoint sirva como: agenda del vet, historial del cliente,
//           citas de una mascota, o listado general con rango de fechas.
// QUIÉN LO USA: CitaController.GetAll → CitaService.ListarCitasAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Cita;

public class CitaFilterRequest
{
    public int? IdVeterinario { get; set; }
    public int? IdCliente { get; set; }
    public int? IdMascota { get; set; }
    // Filtra por estado exacto: "Pendiente", "En Curso", "Completada", "Cancelada"
    public string? Estado { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
}
