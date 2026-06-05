// ═══════════════════════════════════════════════════════
// ARCHIVO: CitaSpResult.cs
// QUÉ HACE: Clase "recipiente" para el resultado de sp_Cita_Read y sp_Cita_List.
//           Los SPs hacen JOIN con MASCOTA, VETERINARIO y CLIENTE para devolver
//           una fila "desnormalizada" con toda la información de la cita en un solo paso.
//           Así el controlador recibe todo listo sin necesitar consultas adicionales.
//           No es una tabla real — se declara como HasNoKey en AppDbContext.
// QUIÉN LO USA: CitaRepository.ObtenerCitaAsync, CitaRepository.ListarCitasAsync
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.Models.SpResults;

// ⚠️ Las propiedades DEBEN ser snake_case para que EF las mapee correctamente desde FromSqlRaw
public class CitaSpResult
{
    public int id_cita { get; set; }
    public int id_mascota { get; set; }
    public string? nombre_mascota { get; set; }
    public string? especie { get; set; }
    public int id_veterinario { get; set; }
    public string? nombre_veterinario { get; set; }
    public string? apellido_veterinario { get; set; }
    public int? id_cliente { get; set; }
    public string? nombre_cliente { get; set; }
    public string? apellido_cliente { get; set; }
    public DateTime fecha_hora { get; set; }
    public string? estado { get; set; }
    public string? motivo { get; set; }
    public string? observaciones { get; set; }
    public DateTime fecha_creacion { get; set; }
}
