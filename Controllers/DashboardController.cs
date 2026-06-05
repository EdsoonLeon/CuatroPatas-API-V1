using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Dashboard;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

// Endpoint del dashboard — PersonalClinica ve métricas de toda la clínica en una sola llamada al SP
[ApiController]
[Route("api/dashboard")]
[Authorize(Policy = "PersonalClinica")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService) => _dashboardService = dashboardService;

    /// <summary>Devuelve métricas globales de la clínica para el rango de fechas indicado</summary>
    [HttpGet]
    public async Task<ActionResult<DashboardResponse>> GetResumen(
        [FromQuery] DateTime? fecha_inicio,
        [FromQuery] DateTime? fecha_fin)
    {
        var result = await _dashboardService.GetResumenAsync(fecha_inicio, fecha_fin);
        return Ok(result);
    }
}
