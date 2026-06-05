// ═══════════════════════════════════════════════════════
// ARCHIVO: Medicamento.cs
// QUÉ HACE: Representa la tabla MEDICAMENTO de la base de datos.
//           Es el inventario de medicamentos disponibles en la clínica.
//           Lleva el stock actual y alerta cuando baja del mínimo permitido.
// QUIÉN LO USA: MedicamentoService, PrescripcionService (al prescribir reduce stock)
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroPatas.API.Models;

[Table("MEDICAMENTO")]
public class Medicamento
{
    [Key]
    [Column("id_medicamento")]
    public int IdMedicamento { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    // Unidades disponibles actualmente. El SP de prescripción lo decrementa cada vez
    // que se receta este medicamento.
    [Column("stock")]
    public int Stock { get; set; } = 0;

    [Column("precio")]
    public decimal Precio { get; set; }

    // Nivel de alerta: cuando Stock <= StockMinimo, el SP sp_Reporte_StockBajo
    // incluye este medicamento en el reporte para que el admin reponga inventario.
    // El valor por defecto de 5 es un umbral conservador para clínicas pequeñas.
    [Column("stock_minimo")]
    public int StockMinimo { get; set; } = 5;

    [Column("activo")]
    public bool Activo { get; set; } = true;

    public ICollection<Prescripcion> Prescripciones { get; set; } = new List<Prescripcion>();
}
