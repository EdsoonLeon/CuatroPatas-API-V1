using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("MASCOTA")]
public class Mascota
{
    [Key]
    [Column("id_mascota")]
    public int IdMascota { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("especie")]
    public string Especie { get; set; } = string.Empty;

    [Column("raza")]
    public string? Raza { get; set; }

    [Column("fecha_nacimiento")]
    public DateOnly? FechaNacimiento { get; set; }

    // SQL Server calcula esta columna automáticamente a partir de fecha_nacimiento.
    [Column("edad_calculada")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int? EdadCalculada { get; set; }

    [Column("peso")]
    public decimal? Peso { get; set; }

    [Column("color")]
    public string? Color { get; set; }

    [Column("sexo")]
    public string? Sexo { get; set; }

    [Column("numero_chip")]
    public string? NumeroChip { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public Cliente Cliente { get; set; } = null!;
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    public ICollection<HistorialMedico> Historiales { get; set; } = new List<HistorialMedico>();
    public ICollection<DocumentoMascota> Documentos { get; set; } = new List<DocumentoMascota>();
}
