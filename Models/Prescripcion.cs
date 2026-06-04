using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("PRESCRIPCION")]
public class Prescripcion
{
    [Key]
    [Column("id_prescripcion")]
    public int IdPrescripcion { get; set; }

    [Column("id_historial")]
    public int IdHistorial { get; set; }

    [Column("id_medicamento")]
    public int IdMedicamento { get; set; }

    [Column("dosis")]
    public string Dosis { get; set; } = string.Empty;

    [Column("frecuencia")]
    public string Frecuencia { get; set; } = string.Empty;

    [Column("duracion")]
    public string Duracion { get; set; } = string.Empty;

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("fecha_prescripcion")]
    public DateTime FechaPrescripcion { get; set; } = DateTime.Now;

    public HistorialMedico HistorialMedico { get; set; } = null!;
    public Medicamento Medicamento { get; set; } = null!;
}
