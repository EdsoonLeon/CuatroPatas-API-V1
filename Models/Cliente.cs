// ═══════════════════════════════════════════════════════
// ARCHIVO: Cliente.cs
// QUÉ HACE: Representa la tabla CLIENTE de la base de datos.
//           Guarda los datos personales del dueño de las mascotas.
//           Un cliente puede tener cuenta de usuario (para acceder al portal)
//           o puede haber sido registrado manualmente por la recepción sin cuenta.
// QUIÉN LO USA: ClienteService, ClienteRepository, AuthService (registro)
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("CLIENTE")]
public class Cliente
{
    [Key]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    // Puede ser null si el cliente fue registrado manualmente por recepción
    // sin haberle creado una cuenta de acceso al sistema.
    // Si tiene cuenta, aquí guarda la FK hacia la tabla USUARIO.
    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("apellido")]
    public string Apellido { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("telefono")]
    public string? Telefono { get; set; }

    [Column("direccion")]
    public string? Direccion { get; set; }

    // En vez de borrar clientes, los marcamos como inactivos para conservar el historial
    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // Si tiene cuenta de usuario, podemos acceder a ella con Include()
    public Usuario? Usuario { get; set; }
    // Todas las mascotas que pertenecen a este cliente
    public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
}
