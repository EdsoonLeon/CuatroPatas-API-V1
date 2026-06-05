// ═══════════════════════════════════════════════════════
// ARCHIVO: CreateMedicamentoRequest.cs
// QUÉ HACE: Datos para agregar un nuevo medicamento al inventario.
//           StockMinimo tiene un default de 5 — umbral conservador para clínicas pequeñas.
// QUIÉN LO USA: MedicamentoController.Create → MedicamentoService.CrearMedicamentoAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Medicamento;

public class CreateMedicamentoRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Required, Range(0.01, double.MaxValue)]
    public decimal Precio { get; set; }

    // Umbral de alerta: cuando Stock ≤ StockMinimo aparece en el reporte de stock bajo
    [Required, Range(0, int.MaxValue)]
    public int StockMinimo { get; set; } = 5;
}
