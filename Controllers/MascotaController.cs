using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Cita;
using CuatroPatas.API.DTOs.Historial;
using CuatroPatas.API.DTOs.Mascota;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

[ApiController]
[Route("api/mascota")]
[Authorize]
public class MascotaController : ControllerBase
{
    private readonly IMascotaService _mascotaService;

    public MascotaController(IMascotaService mascotaService) => _mascotaService = mascotaService;

    [HttpGet]
    public async Task<ActionResult<List<MascotaResponse>>> GetAll()
    {
        var result = await _mascotaService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MascotaResponse>> GetById(int id)
    {
        var result = await _mascotaService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<MascotaResponse>> Create([FromBody] CreateMascotaRequest request)
    {
        var result = await _mascotaService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdMascota }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MascotaResponse>> Update(int id, [FromBody] UpdateMascotaRequest request)
    {
        var result = await _mascotaService.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mascotaService.SoftDeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/historial")]
    public async Task<ActionResult<List<HistorialResponse>>> GetHistorial(int id)
    {
        var result = await _mascotaService.GetHistorialAsync(id);
        return Ok(result);
    }

    [HttpGet("{id}/proximas-citas")]
    public async Task<ActionResult<List<CitaResponse>>> GetProximasCitas(int id)
    {
        var result = await _mascotaService.GetProximasCitasAsync(id);
        return Ok(result);
    }
}
