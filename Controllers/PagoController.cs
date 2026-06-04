using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Pago;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

[ApiController]
[Route("api/pago")]
[Authorize(Policy = "PersonalClinica")]
public class PagoController : ControllerBase
{
    private readonly IPagoService _pagoService;

    public PagoController(IPagoService pagoService) => _pagoService = pagoService;

    [HttpGet("cita/{idCita}")]
    public async Task<ActionResult<PagoResponse>> GetByCita(int idCita)
    {
        var result = await _pagoService.GetByCitaAsync(idCita);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PagoResponse>> Update(int id, [FromBody] UpdatePagoRequest request)
    {
        var result = await _pagoService.UpdateAsync(id, request);
        return Ok(result);
    }
}
