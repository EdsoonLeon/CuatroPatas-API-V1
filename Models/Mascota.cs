// ═══════════════════════════════════════════════════════
// ARCHIVO: Mascota.cs
// QUÉ HACE: Representa la tabla MASCOTA de la base de datos.
//           Guarda los datos de cada animal registrado en la clínica.
//           La edad se calcula automáticamente en SQL Server — la app solo la lee.
// QUIÉN LO USA: MascotaService, MascotaRepository, CitaService
// ═══════════════════════════════════════════════════════

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
    public DateTime? FechaNacimiento { get; set; }

    // Esta columna la calcula SQL Server automáticamente a partir de fecha_nacimiento.
    // El atributo DatabaseGenerated(Computed) le dice a EF que NO intente escribir este campo.
    // Solo se lee — la app nunca la modifica directamente.
    // Después de un INSERT o UPDATE, hacemos ReloadAsync() para traer el valor actualizado.
    [Column("edad_calculada")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int? EdadCalculada { get; set; }

    [Column("sexo")]
    public string? Sexo { get; set; }

    [Column("color")]
    public string? Color { get; set; }

    [Column("peso")]
    public decimal? Peso { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("foto_url")]
    public string? FotoUrl { get; set; }

    public Cliente Cliente { get; set; } = null!;
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    public ICollection<HistorialMedico> Historiales { get; set; } = new List<HistorialMedico>();
    public ICollection<DocumentoMascota> Documentos { get; set; } = new List<DocumentoMascota>();
}
