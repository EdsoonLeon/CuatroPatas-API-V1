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

    [Column("id_cita")]
    public int? IdCita { get; set; }

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
    public ICollection<Prescripcion> Prescripciones { get; set; } = new List<Prescripcion>();
}
