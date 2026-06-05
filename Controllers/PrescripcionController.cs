using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Prescripcion;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

// Endpoints de prescripciones — escritura es VetOAdmin; lectura es acceso general autenticado
[ApiController]
[Route("api/prescripcion")]
public class PrescripcionController : ControllerBase
{
    private readonly IPrescripcionService _prescripcionService;

    public PrescripcionController(IPrescripcionService prescripcionService) => _prescripcionService = prescripcionService;

    /// <summary>Crea una prescripción vinculada a un historial médico — solo VetOAdmin</summary>
    [Authorize(Policy = "VetOAdmin")]
    [HttpPost]
    public async Task<ActionResult<PrescripcionResponse>> Create([FromBody] CreatePrescripcionRequest request)
    {
        var result = await _prescripcionService.CreateAsync(request);
        return CreatedAtAction(nameof(GetByHistorial), new { idHistorial = result.IdHistorial }, result);
    }

    /// <summary>Lista las prescripciones asociadas a un historial médico específico</summary>
    [Authorize]
    [HttpGet("historial/{idHistorial}")]
    public async Task<ActionResult<List<PrescripcionResponse>>> GetByHistorial(int idHistorial)
    {
        var result = await _prescripcionService.GetByHistorialAsync(idHistorial);
        return Ok(result);
    }

    /// <summary>Lista todas las prescripciones activas de una mascota</summary>
    [Authorize]
    [HttpGet("mascota/{idMascota}")]
    public async Task<ActionResult<List<PrescripcionResponse>>> GetByMascota(int idMascota)
    {
        var result = await _prescripcionService.GetByMascotaAsync(idMascota);
        return Ok(result);
    }

    /// <summary>Actualiza instrucciones o fechas de una prescripción — solo VetOAdmin</summary>
    [Authorize(Policy = "VetOAdmin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<PrescripcionResponse>> Update(int id, [FromBody] UpdatePrescripcionRequest request)
    {
        var result = await _prescripcionService.UpdateAsync(id, request);
        return Ok(result);
    }

    /// <summary>Elimina una prescripción — solo VetOAdmin</summary>
    [Authorize(Policy = "VetOAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _prescripcionService.DeleteAsync(id);
        return NoContent();
    }
}
