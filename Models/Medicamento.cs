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

    [Column("stock")]
    public int Stock { get; set; } = 0;

    [Column("precio")]
    public decimal Precio { get; set; }

    [Column("stock_minimo")]
    public int StockMinimo { get; set; } = 5;

    [Column("activo")]
    public bool Activo { get; set; } = true;

    public ICollection<Prescripcion> Prescripciones { get; set; } = new List<Prescripcion>();
}
