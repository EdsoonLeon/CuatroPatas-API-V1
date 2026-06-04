using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Servicio;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

[ApiController]
[Route("api/servicio")]
public class ServicioController : ControllerBase
{
    private readonly IServicioService _servicioService;

    public ServicioController(IServicioService servicioService) => _servicioService = servicioService;

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ServicioResponse>>> GetAll()
    {
        var result = await _servicioService.GetAllAsync();
        return Ok(result);
    }

    [Authorize(Policy = "SoloAdmin")]
    [HttpPost]
    public async Task<ActionResult<ServicioResponse>> Create([FromBody] CreateServicioRequest request)
    {
        var result = await _servicioService.CreateAsync(request);
        return Ok(result);
    }

    [Authorize(Policy = "SoloAdmin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ServicioResponse>> Update(int id, [FromBody] UpdateServicioRequest request)
    {
        var result = await _servicioService.UpdateAsync(id, request);
        return Ok(result);
    }

    [Authorize(Policy = "SoloAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _servicioService.SoftDeleteAsync(id);
        return NoContent();
    }
}
