// ═══════════════════════════════════════════════════════
// ARCHIVO: Cita.cs
// QUÉ HACE: Representa la tabla CITA de la base de datos.
//           Es el registro central de cada atención veterinaria agendada.
//           Tiene un ciclo de vida con estados:
//           Pendiente → En Curso → Completada / Cancelada
// QUIÉN LO USA: CitaService, CitaRepository (vía SPs)
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("CITA")]
public class Cita
{
    [Key]
    [Column("id_cita")]
    public int IdCita { get; set; }

    [Column("id_mascota")]
    public int IdMascota { get; set; }

    [Column("id_veterinario")]
    public int IdVeterinario { get; set; }

    [Column("fecha_hora")]
    public DateTime FechaHora { get; set; }

    // Estado actual de la cita. Los valores posibles son:
    // "Pendiente" (recién creada), "En Curso" (en atención),
    // "Completada" (atención terminada), "Cancelada"
    [Column("estado")]
    public string Estado { get; set; } = "Pendiente";

    [Column("motivo")]
    public string Motivo { get; set; } = string.Empty;

    [Column("observaciones")]
    public string? Observaciones { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public Mascota Mascota { get; set; } = null!;
    public Veterinario Veterinario { get; set; } = null!;
    // Los servicios facturados en esta cita (pueden ser varios)
    public ICollection<DetalleCita> DetallesCita { get; set; } = new List<DetalleCita>();
    // El pago asociado — se crea automáticamente cuando se crea la cita (via SP)
    public Pago? Pago { get; set; }
    // Los registros clínicos generados durante o después de esta cita
    public ICollection<HistorialMedico> Historiales { get; set; } = new List<HistorialMedico>();
}
