using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Historial;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

[ApiController]
[Route("api/historial")]
public class HistorialController : ControllerBase
{
    private readonly IHistorialMedicoService _historialService;

    public HistorialController(IHistorialMedicoService historialService) => _historialService = historialService;

    [Authorize(Policy = "VetOAdmin")]
    [HttpPost]
    public async Task<ActionResult<HistorialResponse>> Create([FromBody] CreateHistorialRequest request)
    {
        var result = await _historialService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdHistorial }, result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<HistorialResponse>> GetById(int id)
    {
        var result = await _historialService.GetByIdAsync(id);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("mascota/{idMascota}")]
    public async Task<ActionResult<List<HistorialResponse>>> GetByMascota(
        int idMascota,
        [FromQuery] string? tipoRegistro,
        [FromQuery] DateTime? fechaInicio,
        [FromQuery] DateTime? fechaFin)
    {
        var result = await _historialService.GetByMascotaAsync(idMascota, tipoRegistro, fechaInicio, fechaFin);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("mascota/{idMascota}/vacunas")]
    public async Task<ActionResult<List<HistorialResponse>>> GetVacunas(int idMascota)
    {
        var result = await _historialService.GetVacunasAsync(idMascota);
        return Ok(result);
    }

    [Authorize(Policy = "VetOAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _historialService.SoftDeleteAsync(id);
        return NoContent();
    }
}
