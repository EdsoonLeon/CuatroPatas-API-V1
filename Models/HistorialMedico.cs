// ═══════════════════════════════════════════════════════
// ARCHIVO: HistorialMedico.cs
// QUÉ HACE: Representa la tabla HISTORIAL_MEDICO de la base de datos.
//           Es el expediente clínico de la mascota: tipo de registro, diagnóstico,
//           tratamiento y notas de cada atención veterinaria.
//           Puede asociarse a una cita agendada o existir de forma independiente.
// QUIÉN LO USA: HistorialService, HistorialRepository
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("HISTORIAL_MEDICO")]
public class HistorialMedico
{
    [Key]
    [Column("id_historial")]
    public int IdHistorial { get; set; }

    [Column("id_mascota")]
    public int IdMascota { get; set; }

    [Column("id_veterinario")]
    public int IdVeterinario { get; set; }

    // Puede ser null si el registro no proviene de una cita agendada
    // (urgencias, vacunación directa, nota de seguimiento posterior)
    [Column("id_cita")]
    public int? IdCita { get; set; }

    // Categoriza el registro: "Consulta", "Vacunación", "Cirugía", "Examen", etc.
    [Column("tipo_registro")]
    public string TipoRegistro { get; set; } = string.Empty;

    [Column("descripcion")]
    public string Descripcion { get; set; } = string.Empty;

    [Column("diagnostico")]
    public string? Diagnostico { get; set; }

    [Column("tratamiento")]
    public string? Tratamiento { get; set; }

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [Column("activo")]
    public bool Activo { get; set; } = true;

    public Mascota Mascota { get; set; } = null!;
    public Veterinario Veterinario { get; set; } = null!;
    public Cita? Cita { get; set; }
    // Medicamentos recetados en esta atención
    public ICollection<Prescripcion> Prescripciones { get; set; } = new List<Prescripcion>();
}
