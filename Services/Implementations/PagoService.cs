using AutoMapper;
using CuatroPatas.API.DTOs.Pago;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class PagoService : IPagoService
{
    private readonly IPagoRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<PagoService> _logger;

    public PagoService(IPagoRepository repo, IMapper mapper, ILogger<PagoService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagoResponse> GetByCitaAsync(int idCita)
    {
        var pago = await _repo.GetByCitaIdAsync(idCita)
            ?? throw new NotFoundException($"Pago para la cita {idCita} no encontrado.");
        return _mapper.Map<PagoResponse>(pago);
    }

    public async Task<PagoResponse> UpdateAsync(int id, UpdatePagoRequest request)
    {
        var pago = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Pago {id} no encontrado.");

        pago.EstadoPago = request.EstadoPago;
        pago.MetodoPago = request.MetodoPago;
        pago.FechaPago = request.FechaPago;

        var actualizado = await _repo.UpdateAsync(pago);
        _logger.LogInformation("Pago {Id} actualizado a estado {Estado}", id, request.EstadoPago);
        return _mapper.Map<PagoResponse>(actualizado);
    }
}
