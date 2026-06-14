namespace CuatroPatas.API.Models.SpResults;

// Resultado de sp_Veterinario_GetStats
public class EstadisticasSpResult
{
    public int citas_completadas { get; set; }
    public int citas_pendientes { get; set; }
    public int citas_confirmadas { get; set; }
    public int citas_canceladas { get; set; }
    public int mascotas_atendidas { get; set; }
}
