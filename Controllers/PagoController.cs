using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Pago;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

// Endpoints de pagos — acceso restringido a PersonalClinica; no hay creación manual (la hace el SP de Cita)
[ApiController]
[Route("api/pago")]
[Authorize(Policy = "PersonalClinica")]
public class PagoController : ControllerBase
{
    private readonly IPagoService _pagoService;

    public PagoController(IPagoService pagoService) => _pagoService = pagoService;

    /// <summary>Devuelve el pago asociado a una cita específica</summary>
    [HttpGet("cita/{idCita}")]
    public async Task<ActionResult<PagoResponse>> GetByCita(int idCita)
    {
        var result = await _pagoService.GetByCitaAsync(idCita);
        return Ok(result);
    }

    /// <summary>Busca pagos por nombre del cliente (búsqueda parcial)</summary>
    [HttpGet("buscar")]
    public async Task<ActionResult<List<PagoClienteResponse>>> BuscarPorCliente([FromQuery] string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return BadRequest("El nombre del cliente es requerido.");
        var result = await _pagoService.GetByClienteNombreAsync(nombre);
        return Ok(result);
    }

    /// <summary>Actualiza el estado o método de pago de una orden</summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PagoResponse>> Update(int id, [FromBody] UpdatePagoRequest request)
    {
        var result = await _pagoService.UpdateAsync(id, request);
        return Ok(result);
    }
}
