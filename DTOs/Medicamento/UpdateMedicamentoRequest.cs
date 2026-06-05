// ═══════════════════════════════════════════════════════
// ARCHIVO: UpdateMedicamentoRequest.cs
// QUÉ HACE: Datos para actualizar un medicamento existente.
//           NO incluye Stock — los cambios de inventario van por endpoints dedicados
//           (/descontar-stock y /reponer-stock) para mantener trazabilidad y auditoría
//           de cada movimiento de stock.
// QUIÉN LO USA: MedicamentoController.Update → MedicamentoService.ActualizarMedicamentoAsync
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Medicamento;

public class UpdateMedicamentoRequest
{
    [Required, MinLength(2)]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required, Range(0.01, double.MaxValue)]
    public decimal Precio { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int StockMinimo { get; set; }
}
