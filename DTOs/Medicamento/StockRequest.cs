// ═══════════════════════════════════════════════════════
// ARCHIVO: StockRequest.cs
// QUÉ HACE: Cantidad a descontar o reponer del stock de un medicamento.
//           Se usa en dos endpoints separados (/descontar-stock y /reponer-stock)
//           para mantener trazabilidad de los movimientos de inventario.
// QUIÉN LO USA: MedicamentoController.DescontarStock, MedicamentoController.ReponerStock
// ═══════════════════════════════════════════════════════

using System.ComponentModel.DataAnnotations;

namespace CuatroPatas.API.DTOs.Medicamento;

public class StockRequest
{
    // Mínimo 1 — no tiene sentido descontar o reponer 0 unidades
    [Required, Range(1, int.MaxValue)]
    public int Cantidad { get; set; }
}
