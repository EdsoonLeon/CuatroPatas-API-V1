using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Servicio;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

// Endpoints del catálogo de servicios — listado es público autenticado; gestión es SoloAdmin
[ApiController]
[Route("api/servicio")]
public class ServicioController : ControllerBase
{
    private readonly IServicioService _servicioService;

    public ServicioController(IServicioService servicioService) => _servicioService = servicioService;

    /// <summary>Lista todos los servicios activos del catálogo</summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ServicioResponse>>> GetAll()
    {
        var result = await _servicioService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>Agrega un nuevo servicio al catálogo — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpPost]
    public async Task<ActionResult<ServicioResponse>> Create([FromBody] CreateServicioRequest request)
    {
        var result = await _servicioService.CreateAsync(request);
        return Ok(result);
    }

    /// <summary>Actualiza precio o descripción de un servicio — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ServicioResponse>> Update(int id, [FromBody] UpdateServicioRequest request)
    {
        var result = await _servicioService.UpdateAsync(id, request);
        return Ok(result);
    }

    /// <summary>Soft delete de un servicio del catálogo — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _servicioService.SoftDeleteAsync(id);
        return NoContent();
    }
}
