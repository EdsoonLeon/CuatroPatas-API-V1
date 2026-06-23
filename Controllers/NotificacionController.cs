using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Notificacion;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

[ApiController]
[Route("api/notificacion")]
[Authorize]
public class NotificacionController : ControllerBase
{
    private readonly INotificacionService _service;

    public NotificacionController(INotificacionService service) => _service = service;

    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Devuelve las últimas 50 notificaciones del usuario autenticado</summary>
    [HttpGet]
    public async Task<ActionResult<List<NotificacionResponse>>> GetMias()
    {
        var result = await _service.GetByUsuarioAsync(CurrentUserId);
        return Ok(result);
    }

    /// <summary>Marca una notificación como leída (solo si pertenece al usuario)</summary>
    [HttpPut("{id}/marcar-leida")]
    public async Task<IActionResult> MarcarLeida(int id)
    {
        await _service.MarcarLeidaAsync(id, CurrentUserId);
        return NoContent();
    }

    /// <summary>Marca todas las notificaciones del usuario como leídas</summary>
    [HttpPut("marcar-todas-leidas")]
    public async Task<IActionResult> MarcarTodasLeidas()
    {
        await _service.MarcarTodasLeidasAsync(CurrentUserId);
        return NoContent();
    }
}
