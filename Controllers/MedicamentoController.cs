using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CuatroPatas.API.DTOs.Medicamento;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Controllers;

// Endpoints de medicamentos — gestión CRUD solo para SoloAdmin; consulta de stock para PersonalClinica
[ApiController]
[Route("api/medicamento")]
public class MedicamentoController : ControllerBase
{
    private readonly IMedicamentoService _medicamentoService;

    public MedicamentoController(IMedicamentoService medicamentoService) => _medicamentoService = medicamentoService;

    /// <summary>Lista todos los medicamentos del inventario — PersonalClinica</summary>
    [Authorize(Policy = "PersonalClinica")]
    [HttpGet]
    public async Task<ActionResult<List<MedicamentoResponse>>> GetAll()
    {
        var result = await _medicamentoService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>Devuelve el detalle de un medicamento por su ID</summary>
    [Authorize(Policy = "PersonalClinica")]
    [HttpGet("{id}")]
    public async Task<ActionResult<MedicamentoResponse>> GetById(int id)
    {
        var result = await _medicamentoService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>Agrega un nuevo medicamento al inventario — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpPost]
    public async Task<ActionResult<MedicamentoResponse>> Create([FromBody] CreateMedicamentoRequest request)
    {
        var result = await _medicamentoService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.IdMedicamento }, result);
    }

    /// <summary>Actualiza datos de un medicamento — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<MedicamentoResponse>> Update(int id, [FromBody] UpdateMedicamentoRequest request)
    {
        var result = await _medicamentoService.UpdateAsync(id, request);
        return Ok(result);
    }

    /// <summary>Soft delete de un medicamento — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _medicamentoService.SoftDeleteAsync(id);
        return NoContent();
    }

    /// <summary>Lista medicamentos cuyo stock está por debajo del mínimo configurado</summary>
    [Authorize(Policy = "PersonalClinica")]
    [HttpGet("stock-bajo")]
    public async Task<ActionResult<List<MedicamentoResponse>>> GetStockBajo()
    {
        var result = await _medicamentoService.GetStockBajoAsync();
        return Ok(result);
    }

    /// <summary>Descuenta unidades del stock al usarse en una prescripción — VetOAdmin</summary>
    [Authorize(Policy = "VetOAdmin")]
    [HttpPost("{id}/descontar-stock")]
    public async Task<IActionResult> DescontarStock(int id, [FromBody] StockRequest request)
    {
        await _medicamentoService.DescontarStockAsync(id, request.Cantidad);
        return NoContent();
    }

    /// <summary>Agrega unidades al stock del medicamento — solo Admin</summary>
    [Authorize(Policy = "SoloAdmin")]
    [HttpPost("{id}/reponer-stock")]
    public async Task<IActionResult> ReponerStock(int id, [FromBody] StockRequest request)
    {
        await _medicamentoService.ReponerStockAsync(id, request.Cantidad);
        return NoContent();
    }
}
