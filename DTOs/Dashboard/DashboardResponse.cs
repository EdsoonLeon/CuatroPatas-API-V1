namespace CuatroPatas.API.DTOs.Dashboard;

public class DashboardResponse
{
    public int TotalCitasHoy { get; set; }
    public int TotalCitasSemana { get; set; }
    public int TotalCitasMes { get; set; }
    public int TotalClientes { get; set; }
    public int TotalMascotas { get; set; }
    public int TotalVeterinarios { get; set; }
    public int CitasPendientes { get; set; }
    public int CitasEnCurso { get; set; }
    public int CitasCompletadasHoy { get; set; }
    public decimal IngresosHoy { get; set; }
    public decimal IngresosMes { get; set; }
    public int MedicamentosStockBajo { get; set; }
}
