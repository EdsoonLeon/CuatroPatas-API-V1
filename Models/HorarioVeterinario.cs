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

    // 1=Lunes ... 7=Domingo (tinyint en SQL)
    [Column("dia_semana")]
    public byte DiaSemana { get; set; }

    [Column("hora_inicio")]
    public TimeOnly HoraInicio { get; set; }

    [Column("hora_fin")]
    public TimeOnly HoraFin { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    public Veterinario Veterinario { get; set; } = null!;
}
