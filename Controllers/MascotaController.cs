using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Cita;
using CuatroPatas.API.DTOs.Historial;
using CuatroPatas.API.DTOs.Mascota;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

// Endpoints de mascotas — todos requieren autenticación mínima; acceso a historial y citas incluido
[ApiController]
[Route("api/mascota")]
[Authorize]
public class MascotaController : ControllerBase
{
    private readonly IMascotaService _mascotaService;

    public MascotaController(IMascotaService mascotaService) => _mascotaService = mascotaService;

    /// <summary>Lista todas las mascotas activas en el sistema</summary>
    [HttpGet]
    public async Task<ActionResult<List<MascotaResponse>>> GetAll()
    {
        var result = await _mascotaService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>Devuelve el perfil de una mascota por su ID</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<MascotaResponse>> GetById(int id)
    {
        var result = await _mascotaService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>Registra una nueva mascota vinculada a un cliente existente</summary>
    [HttpPost]
    public async Task<ActionResult<MascotaResponse>> Create([FromBody] CreateMascotaRequest request)
    {
        var result = await _mascotaService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdMascota }, result);
    }

    /// <summary>Actualiza los datos de una mascota existente</summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<MascotaResponse>> Update(int id, [FromBody] UpdateMascotaRequest request)
    {
        var result = await _mascotaService.UpdateAsync(id, request);
        return Ok(result);
    }

    /// <summary>Soft delete de la mascota (activo = false); no elimina el registro</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mascotaService.SoftDeleteAsync(id);
        return NoContent();
    }

    /// <summary>Devuelve el historial médico completo de la mascota</summary>
    [HttpGet("{id}/historial")]
    public async Task<ActionResult<List<HistorialResponse>>> GetHistorial(int id)
    {
        var result = await _mascotaService.GetHistorialAsync(id);
        return Ok(result);
    }

    /// <summary>Lista las citas futuras pendientes de la mascota</summary>
    [HttpGet("{id}/proximas-citas")]
    public async Task<ActionResult<List<CitaResponse>>> GetProximasCitas(int id)
    {
        var result = await _mascotaService.GetProximasCitasAsync(id);
        return Ok(result);
    }
}
