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
    public string? Frecuencia { get; set; }

    [Column("duracion_dias")]
    public int? DuracionDias { get; set; }

    [Column("indicaciones")]
    public string? Indicaciones { get; set; }

    [Column("fecha_inicio")]
    public DateOnly? FechaInicio { get; set; }

    [Column("fecha_fin")]
    public DateOnly? FechaFin { get; set; }

    public HistorialMedico HistorialMedico { get; set; } = null!;
    public Medicamento Medicamento { get; set; } = null!;
}
