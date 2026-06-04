using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Cita;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

[ApiController]
[Route("api/cita")]
public class CitaController : ControllerBase
{
    private readonly ICitaService _citaService;

    public CitaController(ICitaService citaService) => _citaService = citaService;

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [Authorize(Policy = "PersonalClinica")]
    [HttpGet]
    public async Task<ActionResult<List<CitaResponse>>> GetAll([FromQuery] CitaFilterRequest filter)
    {
        var result = await _citaService.GetAllAsync(filter);
        return Ok(result);
    }

    [Authorize(Policy = "PersonalClinica")]
    [HttpGet("hoy")]
    public async Task<ActionResult<List<CitaResponse>>> GetHoy([FromQuery] int? idVeterinario)
    {
        var result = await _citaService.GetTodayAsync(idVeterinario);
        return Ok(result);
    }

    [Authorize(Roles = "Cliente")]
    [HttpGet("mis-citas")]
    public async Task<ActionResult<List<CitaResponse>>> GetMisCitas()
    {
        var result = await _citaService.GetMisCitasAsync(GetUserId());
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<CitaResponse>> GetById(int id)
    {
        var result = await _citaService.GetByIdAsync(id);
        return Ok(result);
    }

    [Authorize(Roles = "Cliente,Recepcionista,Administrador")]
    [HttpPost]
    public async Task<ActionResult<CitaResponse>> Create([FromBody] CreateCitaRequest request)
    {
        var result = await _citaService.CreateAsync(request, GetUserId());
        return CreatedAtAction(nameof(GetById), new { id = result.IdCita }, result);
    }

    [Authorize(Policy = "PersonalClinica")]
    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> ChangeEstado(int id, [FromBody] ChangeEstadoCitaRequest request)
    {
        await _citaService.ChangeEstadoAsync(id, request, GetUserId());
        return NoContent();
    }

    [Authorize]
    [HttpPost("{id}/cancelar")]
    public async Task<IActionResult> Cancel(int id, [FromBody] CancelCitaRequest request)
    {
        await _citaService.CancelAsync(id, request, GetUserId());
        return NoContent();
    }

    [Authorize(Policy = "PersonalClinica")]
    [HttpPost("{id}/servicio")]
    public async Task<IActionResult> AddServicio(int id, [FromBody] AddServicioCitaRequest request)
    {
        await _citaService.AddServicioAsync(id, request);
        return NoContent();
    }

    [Authorize]
    [HttpGet("{id}/servicios")]
    public async Task<ActionResult<List<CitaServicioResponse>>> GetServicios(int id)
    {
        var result = await _citaService.GetServiciosAsync(id);
        return Ok(result);
    }
}
