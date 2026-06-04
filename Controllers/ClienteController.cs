using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Cliente;
using CuatroPatas.API.DTOs.Mascota;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

[ApiController]
[Route("api/cliente")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService) => _clienteService = clienteService;

    [Authorize(Policy = "PersonalClinica")]
    [HttpGet]
    public async Task<ActionResult<List<ClienteResponse>>> GetAll()
    {
        var result = await _clienteService.GetAllAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteResponse>> GetById(int id)
    {
        var result = await _clienteService.GetByIdAsync(id);
        return Ok(result);
    }

    [Authorize(Policy = "PersonalClinica")]
    [HttpPost]
    public async Task<ActionResult<ClienteResponse>> Create([FromBody] CreateClienteRequest request)
    {
        var result = await _clienteService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdCliente }, result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteResponse>> Update(int id, [FromBody] UpdateClienteRequest request)
    {
        var result = await _clienteService.UpdateAsync(id, request);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}/mascotas")]
    public async Task<ActionResult<List<MascotaResponse>>> GetMascotas(int id)
    {
        var result = await _clienteService.GetMascotasAsync(id);
        return Ok(result);
    }
}
