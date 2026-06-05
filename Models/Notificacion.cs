// ═══════════════════════════════════════════════════════
// ARCHIVO: Notificacion.cs
// QUÉ HACE: Representa la tabla NOTIFICACION de la base de datos.
//           Son mensajes internos del sistema dirigidos a un usuario específico:
//           recordatorios de citas, alertas de stock bajo, confirmaciones, etc.
//           Tiene dos estados independientes: Enviada (procesada por el canal de entrega)
//           y Leida (vista por el usuario en el portal web).
// QUIÉN LO USA: NotificacionService
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("NOTIFICACION")]
public class Notificacion
{
    [Key]
    [Column("id_notificacion")]
    public int IdNotificacion { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("titulo")]
    public string Titulo { get; set; } = string.Empty;

    [Column("mensaje")]
    public string Mensaje { get; set; } = string.Empty;

    // Categoría de la notificación: "Cita", "Stock", "Sistema", "Pago", etc.
    // El frontend puede usar este campo para mostrar un ícono diferente por tipo
    [Column("tipo")]
    public string Tipo { get; set; } = string.Empty;

    // true cuando el usuario abrió/vio la notificación en el portal
    [Column("leida")]
    public bool Leida { get; set; } = false;

    // true cuando el servicio de notificaciones la despachó por el canal elegido
    // (email, push, SMS). Puede ser false si el envío aún está en cola o falló.
    [Column("enviada")]
    public bool Enviada { get; set; } = false;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // Se registra cuando Enviada pasa a true — útil para diagnóstico de demoras
    [Column("fecha_envio")]
    public DateTime? FechaEnvio { get; set; }

    public Usuario Usuario { get; set; } = null!;
}
