// ═══════════════════════════════════════════════════════
// ARCHIVO: ServicioService.cs
// QUÉ HACE: El "empleado del catálogo" que gestiona los servicios que ofrece la clínica
//           (consulta, vacunación, cirugía, baño, corte de uñas, etc.).
//           CRUD simple — este módulo no tiene lógica de negocio compleja
//           porque el precio se "congela" en DetalleCita cuando se usa en una cita,
//           así que actualizar un precio aquí no afecta citas pasadas.
// QUIÉN LO USA: ServicioController (inyectado como IServicioService)
// ═══════════════════════════════════════════════════════

using AutoMapper;
using CuatroPatas.API.DTOs.Servicio;
using CuatroPatas.API.Helpers;
using CuatroPatas.API.Models;
using CuatroPatas.API.Repositories.Interfaces;
using CuatroPatas.API.Services.Interfaces;

namespace CuatroPatas.API.Services.Implementations;

public class ServicioService : IServicioService
{
    private readonly IServicioRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<ServicioService> _logger;

    public ServicioService(IServicioRepository repo, IMapper mapper, ILogger<ServicioService> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ServicioResponse>> GetAllAsync()
    {
        var servicios = await _repo.GetAllAsync();
        return _mapper.Map<List<ServicioResponse>>(servicios);
    }

    public async Task<ServicioResponse> CreateAsync(CreateServicioRequest request)
    {
        var servicio = _mapper.Map<Servicio>(request);
        var creado = await _repo.CreateAsync(servicio);
        _logger.LogInformation("Servicio creado: {Id}", creado.IdServicio);
        return _mapper.Map<ServicioResponse>(creado);
    }

    public async Task<ServicioResponse> UpdateAsync(int id, UpdateServicioRequest request)
    {
        var servicio = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Servicio {id} no encontrado.");
        _mapper.Map(request, servicio);
        var actualizado = await _repo.UpdateAsync(servicio);
        return _mapper.Map<ServicioResponse>(actualizado);
    }

    public async Task SoftDeleteAsync(int id)
    {
        _ = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"Servicio {id} no encontrado.");
        await _repo.SoftDeleteAsync(id);
    }
}
