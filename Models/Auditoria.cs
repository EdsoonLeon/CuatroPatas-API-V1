// ═══════════════════════════════════════════════════════
// ARCHIVO: Auditoria.cs
// QUÉ HACE: Representa la tabla AUDITORIA de la base de datos.
//           Es la "bitácora de seguridad" del sistema: registra cada acción importante
//           (quién la hizo, qué tabla modificó, qué cambió y desde qué IP).
//           Los SPs de la base de datos insertan automáticamente estos registros.
//           IdUsuario es null cuando la acción la ejecuta el propio sistema (tareas programadas).
// QUIÉN LO USA: Solo lectura vía AuditoriaService — los SPs escriben directamente en esta tabla
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("AUDITORIA")]
public class Auditoria
{
    [Key]
    [Column("id_auditoria")]
    public int IdAuditoria { get; set; }

    // Quién realizó la acción. Null si la ejecutó el sistema automáticamente
    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    // Qué se hizo: "INSERT", "UPDATE", "DELETE", "LOGIN", "LOGOUT", etc.
    [Column("accion")]
    public string Accion { get; set; } = string.Empty;

    // En qué tabla de la BD se realizó la acción: "CITA", "USUARIO", "PAGO", etc.
    [Column("tabla")]
    public string Tabla { get; set; } = string.Empty;

    // El ID del registro afectado (null si la acción no aplica a un registro específico)
    [Column("id_registro")]
    public int? IdRegistro { get; set; }

    // Estado del registro ANTES del cambio — guardado como JSON. Null en INSERTs.
    [Column("datos_anteriores")]
    public string? DatosAnteriores { get; set; }

    // Estado del registro DESPUÉS del cambio — guardado como JSON. Null en DELETEs.
    [Column("datos_nuevos")]
    public string? DatosNuevos { get; set; }

    [Column("fecha_accion")]
    public DateTime FechaAccion { get; set; } = DateTime.Now;

    // La IP desde donde se originó la acción — útil para detectar accesos sospechosos
    [Column("ip_address")]
    public string? IpAddress { get; set; }
}
