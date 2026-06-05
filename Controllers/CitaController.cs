using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Cita;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

// Endpoints de citas — políticas: PersonalClinica para gestión, Cliente solo para sus propias citas
[ApiController]
[Route("api/cita")]
public class CitaController : ControllerBase
{
    private readonly ICitaService _citaService;

    public CitaController(ICitaService citaService) => _citaService = citaService;

    // Extrae IdUsuario del claim del JWT para pasarlo a los SPs de auditoría
    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Lista citas con filtros opcionales — solo personal de clínica puede ver todas</summary>
    [Authorize(Policy = "PersonalClinica")]
    [HttpGet]
    public async Task<ActionResult<List<CitaResponse>>> GetAll([FromQuery] CitaFilterRequest filter)
    {
        var result = await _citaService.GetAllAsync(filter);
        return Ok(result);
    }

    /// <summary>Lista las citas del día; filtra por veterinario si se pasa idVeterinario</summary>
    [Authorize(Policy = "PersonalClinica")]
    [HttpGet("hoy")]
    public async Task<ActionResult<List<CitaResponse>>> GetHoy([FromQuery] int? idVeterinario)
    {
        var result = await _citaService.GetTodayAsync(idVeterinario);
        return Ok(result);
    }

    /// <summary>Devuelve solo las citas del cliente autenticado resolviendo su perfil por IdUsuario</summary>
    [Authorize(Roles = "Cliente")]
    [HttpGet("mis-citas")]
    public async Task<ActionResult<List<CitaResponse>>> GetMisCitas()
    {
        var result = await _citaService.GetMisCitasAsync(GetUserId());
        return Ok(result);
    }

    /// <summary>Devuelve el detalle completo de una cita por su ID</summary>
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<CitaResponse>> GetById(int id)
    {
        var result = await _citaService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>Crea una nueva cita; registra el IdUsuario del creador para auditoría</summary>
    [Authorize(Roles = "Cliente,Recepcionista,Administrador")]
    [HttpPost]
    public async Task<ActionResult<CitaResponse>> Create([FromBody] CreateCitaRequest request)
    {
        var result = await _citaService.CreateAsync(request, GetUserId());
        return CreatedAtAction(nameof(GetById), new { id = result.IdCita }, result);
    }

    /// <summary>Cambia el estado de la cita (Confirmada, Completada, etc.)</summary>
    [Authorize(Policy = "PersonalClinica")]
    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> ChangeEstado(int id, [FromBody] ChangeEstadoCitaRequest request)
    {
        await _citaService.ChangeEstadoAsync(id, request, GetUserId());
        return NoContent();
    }

    /// <summary>Cancela la cita registrando el motivo; audita el IdUsuario que la canceló</summary>
    [Authorize]
    [HttpPost("{id}/cancelar")]
    public async Task<IActionResult> Cancel(int id, [FromBody] CancelCitaRequest request)
    {
        await _citaService.CancelAsync(id, request, GetUserId());
        return NoContent();
    }

    /// <summary>Agrega un servicio adicional a una cita existente</summary>
    [Authorize(Policy = "PersonalClinica")]
    [HttpPost("{id}/servicio")]
    public async Task<IActionResult> AddServicio(int id, [FromBody] AddServicioCitaRequest request)
    {
        await _citaService.AddServicioAsync(id, request);
        return NoContent();
    }

    /// <summary>Lista todos los servicios vinculados a una cita</summary>
    [Authorize]
    [HttpGet("{id}/servicios")]
    public async Task<ActionResult<List<CitaServicioResponse>>> GetServicios(int id)
    {
        var result = await _citaService.GetServiciosAsync(id);
        return Ok(result);
    }
}
