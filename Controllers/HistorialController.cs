using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Historial;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

// Endpoints de historial médico — solo VetOAdmin puede crear y eliminar; cualquier usuario autenticado puede leer
[ApiController]
[Route("api/historial")]
public class HistorialController : ControllerBase
{
    private readonly IHistorialMedicoService _historialService;

    public HistorialController(IHistorialMedicoService historialService) => _historialService = historialService;

    /// <summary>Crea un registro en el historial médico de la mascota — solo VetOAdmin</summary>
    [Authorize(Policy = "VetOAdmin")]
    [HttpPost]
    public async Task<ActionResult<HistorialResponse>> Create([FromBody] CreateHistorialRequest request)
    {
        var result = await _historialService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdHistorial }, result);
    }

    /// <summary>Devuelve un registro de historial por su ID</summary>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<HistorialResponse>> GetById(int id)
    {
        var result = await _historialService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>Lista el historial de una mascota con filtros opcionales de tipo y fecha</summary>
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

    /// <summary>Devuelve solo los registros de vacunación de la mascota</summary>
    [Authorize]
    [HttpGet("mascota/{idMascota}/vacunas")]
    public async Task<ActionResult<List<HistorialResponse>>> GetVacunas(int idMascota)
    {
        var result = await _historialService.GetVacunasAsync(idMascota);
        return Ok(result);
    }

    /// <summary>Soft delete de un registro de historial — solo VetOAdmin</summary>
    [Authorize(Policy = "VetOAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _historialService.SoftDeleteAsync(id);
        return NoContent();
    }
}
