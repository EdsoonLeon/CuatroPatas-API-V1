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

    [Column("duracion_minutos")]
    public int DuracionMinutos { get; set; } = 30;

    [Column("motivo")]
    public string? Motivo { get; set; }

    [Column("estado")]
    public string Estado { get; set; } = "Pendiente";

    [Column("observaciones")]
    public string? Observaciones { get; set; }

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("fecha_modificacion")]
    public DateTime? FechaModificacion { get; set; }

    public Mascota Mascota { get; set; } = null!;
    public Veterinario Veterinario { get; set; } = null!;
    public ICollection<DetalleCita> DetallesCita { get; set; } = new List<DetalleCita>();
    public Pago? Pago { get; set; }
    public ICollection<HistorialMedico> Historiales { get; set; } = new List<HistorialMedico>();
}
