using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("MEDICAMENTO")]
public class Medicamento
{
    [Key]
    [Column("id_medicamento")]
    public int IdMedicamento { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("tipo")]
    public string? Tipo { get; set; }

    [Column("presentacion")]
    public string? Presentacion { get; set; }

    [Column("stock")]
    public int Stock { get; set; } = 0;

    [Column("precio_unitario")]
    public decimal PrecioUnitario { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public ICollection<Prescripcion> Prescripciones { get; set; } = new List<Prescripcion>();
}
