using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Medicamento;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

[ApiController]
[Route("api/medicamento")]
public class MedicamentoController : ControllerBase
{
    private readonly IMedicamentoService _medicamentoService;

    public MedicamentoController(IMedicamentoService medicamentoService) => _medicamentoService = medicamentoService;

    [Authorize(Policy = "PersonalClinica")]
    [HttpGet]
    public async Task<ActionResult<List<MedicamentoResponse>>> GetAll()
    {
        var result = await _medicamentoService.GetAllAsync();
        return Ok(result);
    }

    [Authorize(Policy = "PersonalClinica")]
    [HttpGet("{id}")]
    public async Task<ActionResult<MedicamentoResponse>> GetById(int id)
    {
        var result = await _medicamentoService.GetByIdAsync(id);
        return Ok(result);
    }

    [Authorize(Policy = "SoloAdmin")]
    [HttpPost]
    public async Task<ActionResult<MedicamentoResponse>> Create([FromBody] CreateMedicamentoRequest request)
    {
        var result = await _medicamentoService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdMedicamento }, result);
    }

    [Authorize(Policy = "SoloAdmin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<MedicamentoResponse>> Update(int id, [FromBody] UpdateMedicamentoRequest request)
    {
        var result = await _medicamentoService.UpdateAsync(id, request);
        return Ok(result);
    }

    [Authorize(Policy = "SoloAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _medicamentoService.SoftDeleteAsync(id);
        return NoContent();
    }

    [Authorize(Policy = "PersonalClinica")]
    [HttpGet("stock-bajo")]
    public async Task<ActionResult<List<MedicamentoResponse>>> GetStockBajo()
    {
        var result = await _medicamentoService.GetStockBajoAsync();
        return Ok(result);
    }

    [Authorize(Policy = "VetOAdmin")]
    [HttpPost("{id}/descontar-stock")]
    public async Task<IActionResult> DescontarStock(int id, [FromBody] StockRequest request)
    {
        await _medicamentoService.DescontarStockAsync(id, request.Cantidad);
        return NoContent();
    }

    [Authorize(Policy = "SoloAdmin")]
    [HttpPost("{id}/reponer-stock")]
    public async Task<IActionResult> ReponerStock(int id, [FromBody] StockRequest request)
    {
        await _medicamentoService.ReponerStockAsync(id, request.Cantidad);
        return NoContent();
    }
}
