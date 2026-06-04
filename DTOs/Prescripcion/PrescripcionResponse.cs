namespace CuatroPatas.API.DTOs.Prescripcion;

public class PrescripcionResponse
{
    public int IdPrescripcion { get; set; }
    public int IdHistorial { get; set; }
    public int IdMedicamento { get; set; }
    public string NombreMedicamento { get; set; } = string.Empty;
    public string Dosis { get; set; } = string.Empty;
    public string Frecuencia { get; set; } = string.Empty;
    public string Duracion { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public DateTime FechaPrescripcion { get; set; }
}
