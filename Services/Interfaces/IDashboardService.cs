using CuatroPatas.API.DTOs.Dashboard;

namespace CuatroPatas.API.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardResponse> GetResumenAsync(DateTime? fechaInicio, DateTime? fechaFin);
}
