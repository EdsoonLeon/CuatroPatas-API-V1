namespace CuatroPatas.API.DTOs.Veterinario;

public class EstadisticasResponse
{
    public int TotalCitas { get; set; }
    public int CitasCompletadas { get; set; }
    public int CitasCanceladas { get; set; }
    public int CitasPendientes { get; set; }
    public decimal IngresosTotal { get; set; }
    public string? Periodo { get; set; }
}
