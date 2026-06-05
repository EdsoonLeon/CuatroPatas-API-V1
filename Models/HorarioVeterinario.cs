// ═══════════════════════════════════════════════════════
// ARCHIVO: HorarioVeterinario.cs
// QUÉ HACE: Representa la tabla HORARIO_VETERINARIO de la base de datos.
//           Guarda las franjas horarias de disponibilidad semanal de cada veterinario.
//           Ejemplo: "Lunes de 09:00 a 17:00", "Miércoles de 14:00 a 18:00".
//           Los Stored Procedures de agenda consultan esta tabla para verificar disponibilidad.
// QUIÉN LO USA: VeterinarioService (agenda), Stored Procedures de citas
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("HORARIO_VETERINARIO")]
public class HorarioVeterinario
{
    [Key]
    [Column("id_horario")]
    public int IdHorario { get; set; }

    [Column("id_veterinario")]
    public int IdVeterinario { get; set; }

    // El día de la semana en texto ("Lunes", "Martes", etc.)
    [Column("dia_semana")]
    public string DiaSemana { get; set; } = string.Empty;

    // Hora de inicio de la jornada — TimeOnly representa solo la hora, sin fecha
    [Column("hora_inicio")]
    public TimeOnly HoraInicio { get; set; }

    // Hora de fin de la jornada
    [Column("hora_fin")]
    public TimeOnly HoraFin { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    public Veterinario Veterinario { get; set; } = null!;
}
