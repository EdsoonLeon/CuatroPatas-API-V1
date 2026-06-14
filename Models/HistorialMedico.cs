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

    [Column("id_cita")]
    public int? IdCita { get; set; }

    [Column("id_veterinario")]
    public int IdVeterinario { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("tipo")]
    public string Tipo { get; set; } = string.Empty;

    [Column("diagnostico")]
    public string? Diagnostico { get; set; }

    [Column("tratamiento")]
    public string? Tratamiento { get; set; }

    [Column("observaciones")]
    public string? Observaciones { get; set; }

    [Column("peso")]
    public decimal? Peso { get; set; }

    [Column("temperatura")]
    public decimal? Temperatura { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public Mascota Mascota { get; set; } = null!;
    public Veterinario Veterinario { get; set; } = null!;
    public Cita? Cita { get; set; }
    public ICollection<Prescripcion> Prescripciones { get; set; } = new List<Prescripcion>();
}
