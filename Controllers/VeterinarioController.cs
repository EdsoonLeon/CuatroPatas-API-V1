using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Veterinario;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

// Endpoints de veterinarios — Create/Update/Delete son SoloAdmin; agenda y estadísticas tienen acceso diferenciado
[ApiController]
[Route("api/veterinario")]
public class VeterinarioController : ControllerBase
{
    private readonly IVeterinarioService _veterinarioService;

    public VeterinarioController(IVeterinarioService veterinarioService) => _veterinarioService = veterinarioService;

    /// <summary>Lista todos los veterinarios activos del sistema</summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<VeterinarioResponse>>> GetAll()
    {
        var result = await _veterinarioService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>Devuelve el perfil de un veterinario por su ID</summary>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<VeterinarioResponse>> GetById(int id)
    {
        var result = await _veterinarioService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>Registra un nuevo veterinario y crea su usuario asociado — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpPost]
    public async Task<ActionResult<VeterinarioResponse>> Create([FromBody] CreateVeterinarioRequest request)
    {
        var result = await _veterinarioService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdVeterinario }, result);
    }

    /// <summary>Actualiza los datos del veterinario — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<VeterinarioResponse>> Update(int id, [FromBody] UpdateVeterinarioRequest request)
    {
        var result = await _veterinarioService.UpdateAsync(id, request);
        return Ok(result);
    }

    /// <summary>Soft delete del veterinario — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _veterinarioService.SoftDeleteAsync(id);
        return NoContent();
    }

    /// <summary>Devuelve la agenda del veterinario para el rango de fechas indicado</summary>
    [Authorize(Policy = "PersonalClinica")]
    [HttpGet("{id}/agenda")]
    public async Task<ActionResult<List<AgendaResponse>>> GetAgenda(
        int id,
        [FromQuery] DateTime fechaInicio,
        [FromQuery] DateTime fechaFin)
    {
        var result = await _veterinarioService.GetAgendaAsync(id, fechaInicio, fechaFin);
        return Ok(result);
    }

    /// <summary>Devuelve estadísticas de atención del veterinario en el rango dado</summary>
    [Authorize(Policy = "VetOAdmin")]
    [HttpGet("{id}/estadisticas")]
    public async Task<ActionResult<EstadisticasResponse>> GetEstadisticas(
        int id,
        [FromQuery] DateTime? fechaInicio,
        [FromQuery] DateTime? fechaFin)
    {
        var result = await _veterinarioService.GetEstadisticasAsync(id, fechaInicio, fechaFin);
        return Ok(result);
    }
}
