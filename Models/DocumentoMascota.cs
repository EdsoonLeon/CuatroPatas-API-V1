// ═══════════════════════════════════════════════════════
// ARCHIVO: DocumentoMascota.cs
// QUÉ HACE: Representa la tabla DOCUMENTO_MASCOTA de la base de datos.
//           Guarda referencias a archivos adjuntos de una mascota:
//           radiografías, análisis de laboratorio, certificados de vacuna, etc.
//           NO guarda el archivo en la BD — solo guarda la URL donde está alojado
//           (blob de Azure, S3, carpeta del servidor, etc.).
// QUIÉN LO USA: DocumentoService, MascotaService
// ═══════════════════════════════════════════════════════

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

    // Categoría del archivo: "Radiografía", "Análisis", "Certificado Vacuna", etc.
    // Facilita la búsqueda y el filtrado en el historial documental
    [Column("tipo_documento")]
    public string TipoDocumento { get; set; } = string.Empty;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    // URL del archivo — la BD guarda solo la referencia; el archivo vive fuera de la BD
    [Column("url_archivo")]
    public string UrlArchivo { get; set; } = string.Empty;

    [Column("fecha_subida")]
    public DateTime FechaSubida { get; set; } = DateTime.Now;

    public Mascota Mascota { get; set; } = null!;
}
