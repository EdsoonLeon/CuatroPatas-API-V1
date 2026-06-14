namespace CuatroPatas.API.DTOs.Prescripcion;

public class PrescripcionResponse
{
    public int IdPrescripcion { get; set; }
    public int IdHistorial { get; set; }
    public int IdMedicamento { get; set; }
    public string NombreMedicamento { get; set; } = string.Empty;
    public string Dosis { get; set; } = string.Empty;
    public string? Frecuencia { get; set; }
    public int? DuracionDias { get; set; }
    public string? Indicaciones { get; set; }
    public DateOnly? FechaInicio { get; set; }
    public DateOnly? FechaFin { get; set; }
    public string? NombreVeterinario { get; set; }
}
