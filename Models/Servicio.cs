// ═══════════════════════════════════════════════════════
// ARCHIVO: Servicio.cs
// QUÉ HACE: Representa la tabla SERVICIO de la base de datos.
//           Es el catálogo de todos los servicios que ofrece la clínica
//           (consulta, vacuna, cirugía, baño, etc.) con su precio y duración.
// QUIÉN LO USA: ServicioService, CitaService (al agregar servicios a una cita)
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("SERVICIO")]
public class Servicio
{
    [Key]
    [Column("id_servicio")]
    public int IdServicio { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    // Precio actual del servicio — al agregarlo a una cita, el SP copia este precio
    // a DetalleCita.PrecioUnitario para "congelar" el precio en el momento de la atención
    [Column("precio")]
    public decimal Precio { get; set; }

    // Duración estimada en minutos — útil para calcular disponibilidad y agenda
    [Column("duracion_minutos")]
    public int DuracionMinutos { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    public ICollection<DetalleCita> DetallesCita { get; set; } = new List<DetalleCita>();
}
