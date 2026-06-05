// ═══════════════════════════════════════════════════════
// ARCHIVO: MedicamentoResponse.cs
// QUÉ HACE: Datos de un medicamento para mostrar en el frontend.
//           StockBajo es una propiedad calculada en el DTO — el frontend puede
//           mostrar directamente una alerta visual sin necesitar lógica extra.
// QUIÉN LO USA: MedicamentoController en todas las respuestas de lectura
// ═══════════════════════════════════════════════════════

namespace CuatroPatas.API.DTOs.Medicamento;

public class MedicamentoResponse
{
    public int IdMedicamento { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int Stock { get; set; }
    public decimal Precio { get; set; }
    public int StockMinimo { get; set; }
    // true cuando Stock ≤ StockMinimo — el frontend puede mostrar un badge de alerta sin lógica adicional
    public bool StockBajo => Stock <= StockMinimo;
}
