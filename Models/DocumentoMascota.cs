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

    [Column("id_historial")]
    public int? IdHistorial { get; set; }

    [Column("tipo_doc")]
    public string TipoDoc { get; set; } = string.Empty;

    [Column("nombre_archivo")]
    public string NombreArchivo { get; set; } = string.Empty;

    [Column("url_storage")]
    public string UrlStorage { get; set; } = string.Empty;

    [Column("tamano_bytes")]
    public int? TamanoBytes { get; set; }

    [Column("subido_por")]
    public int SubidoPor { get; set; }

    [Column("fecha_subida")]
    public DateTime FechaSubida { get; set; } = DateTime.Now;

    public Mascota Mascota { get; set; } = null!;
}
