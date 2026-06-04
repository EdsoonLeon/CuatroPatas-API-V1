using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Prescripcion;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

[ApiController]
[Route("api/prescripcion")]
public class PrescripcionController : ControllerBase
{
    private readonly IPrescripcionService _prescripcionService;

    public PrescripcionController(IPrescripcionService prescripcionService) => _prescripcionService = prescripcionService;

    [Authorize(Policy = "VetOAdmin")]
    [HttpPost]
    public async Task<ActionResult<PrescripcionResponse>> Create([FromBody] CreatePrescripcionRequest request)
    {
        var result = await _prescripcionService.CreateAsync(request);
        return CreatedAtAction(nameof(GetByHistorial), new { idHistorial = result.IdHistorial }, result);
    }

    [Authorize]
    [HttpGet("historial/{idHistorial}")]
    public async Task<ActionResult<List<PrescripcionResponse>>> GetByHistorial(int idHistorial)
    {
        var result = await _prescripcionService.GetByHistorialAsync(idHistorial);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("mascota/{idMascota}")]
    public async Task<ActionResult<List<PrescripcionResponse>>> GetByMascota(int idMascota)
    {
        var result = await _prescripcionService.GetByMascotaAsync(idMascota);
        return Ok(result);
    }

    [Authorize(Policy = "VetOAdmin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<PrescripcionResponse>> Update(int id, [FromBody] UpdatePrescripcionRequest request)
    {
        var result = await _prescripcionService.UpdateAsync(id, request);
        return Ok(result);
    }

    [Authorize(Policy = "VetOAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _prescripcionService.DeleteAsync(id);
        return NoContent();
    }
}
