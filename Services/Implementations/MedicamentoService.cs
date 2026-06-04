using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CuatroPatas.API.Data;
using CuatroPatas.API.DTOs.Medicamento;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Models;
using CuatroPatas.API.Models.SpResults;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class MedicamentoService : IMedicamentoService
{
    private readonly IMedicamentoRepository _repo;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<MedicamentoService> _logger;

    public MedicamentoService(
        IMedicamentoRepository repo,
        AppDbContext context,
        IMapper mapper,
        ILogger<MedicamentoService> logger)
    {
        _repo = repo;
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<MedicamentoResponse>> GetAllAsync()
    {
        var meds = await _repo.GetAllAsync();
        return _mapper.Map<List<MedicamentoResponse>>(meds);
    }

    public async Task<MedicamentoResponse> GetByIdAsync(int id)
    {
        var med = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Medicamento {id} no encontrado.");
        return _mapper.Map<MedicamentoResponse>(med);
    }

    public async Task<MedicamentoResponse> CreateAsync(CreateMedicamentoRequest request)
    {
        var med = _mapper.Map<Medicamento>(request);
        var creado = await _repo.CreateAsync(med);
        _logger.LogInformation("Medicamento creado: {Id}", creado.IdMedicamento);
        return _mapper.Map<MedicamentoResponse>(creado);
    }

    public async Task<MedicamentoResponse> UpdateAsync(int id, UpdateMedicamentoRequest request)
    {
        var med = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Medicamento {id} no encontrado.");
        _mapper.Map(request, med);
        var actualizado = await _repo.UpdateAsync(med);
        return _mapper.Map<MedicamentoResponse>(actualizado);
    }

    public async Task SoftDeleteAsync(int id)
    {
        _ = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Medicamento {id} no encontrado.");
        await _repo.SoftDeleteAsync(id);
    }

    public async Task<List<MedicamentoResponse>> GetStockBajoAsync()
    {
        var results = await _context.Set<StockBajoSpResult>()
            .FromSqlRaw("EXEC sp_Medicamento_ListStockBajo")
            .ToListAsync();

        return results.Select(r => new MedicamentoResponse
        {
            IdMedicamento = r.id_medicamento,
            Nombre = r.nombre ?? string.Empty,
            Stock = r.stock,
            StockMinimo = r.stock_minimo,
            Precio = r.precio
        }).ToList();
    }

    public async Task DescontarStockAsync(int id, int cantidad)
    {
        _ = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Medicamento {id} no encontrado.");

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Medicamento_DescontarStock @id_medicamento, @cantidad",
            new SqlParameter("@id_medicamento", id),
            new SqlParameter("@cantidad", cantidad));

        _logger.LogInformation("Stock descontado: medicamento {Id}, cantidad {Cantidad}", id, cantidad);
    }

    public async Task ReponerStockAsync(int id, int cantidad)
    {
        _ = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Medicamento {id} no encontrado.");

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Medicamento_ReponerStock @id_medicamento, @cantidad",
            new SqlParameter("@id_medicamento", id),
            new SqlParameter("@cantidad", cantidad));

        _logger.LogInformation("Stock repuesto: medicamento {Id}, cantidad {Cantidad}", id, cantidad);
    }
}
