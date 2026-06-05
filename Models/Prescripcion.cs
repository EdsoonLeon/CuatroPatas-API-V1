// ═══════════════════════════════════════════════════════
// ARCHIVO: Prescripcion.cs
// QUÉ HACE: Representa la tabla PRESCRIPCION de la base de datos.
//           Es la receta médica dentro de un historial: qué medicamento,
//           a qué dosis, con qué frecuencia, por cuánto tiempo y cuántas unidades.
//           Al crear una prescripción, el SP descuenta del stock del medicamento.
// QUIÉN LO USA: PrescripcionService, HistorialService
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("PRESCRIPCION")]
public class Prescripcion
{
    [Key]
    [Column("id_prescripcion")]
    public int IdPrescripcion { get; set; }

    [Column("id_historial")]
    public int IdHistorial { get; set; }

    [Column("id_medicamento")]
    public int IdMedicamento { get; set; }

    // Ejemplo: "10 mg", "2 tabletas", "5 ml" — cuánto se administra por toma
    [Column("dosis")]
    public string Dosis { get; set; } = string.Empty;

    // Ejemplo: "cada 8 horas", "una vez al día" — periodicidad de administración
    [Column("frecuencia")]
    public string Frecuencia { get; set; } = string.Empty;

    // Ejemplo: "7 días", "2 semanas" — tiempo total del tratamiento
    [Column("duracion")]
    public string Duracion { get; set; } = string.Empty;

    // Total de unidades que se despachan — el SP descuenta esta cantidad del stock
    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("fecha_prescripcion")]
    public DateTime FechaPrescripcion { get; set; } = DateTime.Now;

    public HistorialMedico HistorialMedico { get; set; } = null!;
    public Medicamento Medicamento { get; set; } = null!;
}
