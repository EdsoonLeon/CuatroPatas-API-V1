using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("DOCUMENTO_MASCOTA")]
public class DocumentoMascota
{
    [Key]
    [Column("id_documento")]
    public int IdDocumento { get; set; }

    [Column("id_mascota")]
    public int IdMascota { get; set; }

    [Column("tipo_documento")]
    public string TipoDocumento { get; set; } = string.Empty;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("url_archivo")]
    public string UrlArchivo { get; set; } = string.Empty;

    [Column("fecha_subida")]
    public DateTime FechaSubida { get; set; } = DateTime.Now;

    public Mascota Mascota { get; set; } = null!;
}
