using AutoMapper;
using CuatroPatas.API.DTOs.Cliente;
using CuatroPatas.API.DTOs.Mascota;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<ClienteService> _logger;

    public ClienteService(IClienteRepository repo, IMapper mapper, ILogger<ClienteService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ClienteResponse>> GetAllAsync()
    {
        var clientes = await _repo.GetAllAsync();
        return _mapper.Map<List<ClienteResponse>>(clientes);
    }

    public async Task<ClienteResponse> GetByIdAsync(int id)
    {
        var cliente = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Cliente {id} no encontrado.");
        return _mapper.Map<ClienteResponse>(cliente);
    }

    public async Task<ClienteResponse> CreateAsync(CreateClienteRequest request)
    {
        var existe = await _repo.GetByEmailAsync(request.Email);
        if (existe != null)
            throw new ConflictException("Ya existe un cliente con ese email.");

        var cliente = _mapper.Map<Cliente>(request);
        cliente.FechaRegistro = DateTime.Now;

        var creado = await _repo.CreateAsync(cliente);
        _logger.LogInformation("Cliente creado: {Id}", creado.IdCliente);
        return _mapper.Map<ClienteResponse>(creado);
    }

    public async Task<ClienteResponse> UpdateAsync(int id, UpdateClienteRequest request)
    {
        var cliente = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Cliente {id} no encontrado.");

        _mapper.Map(request, cliente);
        var actualizado = await _repo.UpdateAsync(cliente);
        return _mapper.Map<ClienteResponse>(actualizado);
    }

    public async Task SoftDeleteAsync(int id)
    {
        _ = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Cliente {id} no encontrado.");
        await _repo.SoftDeleteAsync(id);
        _logger.LogInformation("Cliente {Id} desactivado.", id);
    }

    public async Task<List<MascotaResponse>> GetMascotasAsync(int idCliente)
    {
        _ = await _repo.GetByIdAsync(idCliente)
            ?? throw new NotFoundException($"Cliente {idCliente} no encontrado.");
        var mascotas = await _repo.GetMascotasAsync(idCliente);
        return _mapper.Map<List<MascotaResponse>>(mascotas);
    }
}
