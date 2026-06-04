using AutoMapper;
using CuatroPatas.API.DTOs.Cliente;
using CuatroPatas.API.DTOs.Mascota;
using CuatroPatas.API.DTOs.Medicamento;
using CuatroPatas.API.DTOs.Pago;
using CuatroPatas.API.DTOs.Prescripcion;
using CuatroPatas.API.DTOs.Servicio;
using CuatroPatas.API.DTOs.Veterinario;
using CuatroPatas.API.Models;

namespace CuatroPatas.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Cliente
        CreateMap<Cliente, ClienteResponse>();
        CreateMap<CreateClienteRequest, Cliente>();
        CreateMap<UpdateClienteRequest, Cliente>();

        // Veterinario
        CreateMap<Veterinario, VeterinarioResponse>();
        CreateMap<CreateVeterinarioRequest, Veterinario>()
            .ForMember(d => d.IdUsuario, opt => opt.Ignore())
            .ForMember(d => d.FechaRegistro, opt => opt.Ignore())
            .ForMember(d => d.Activo, opt => opt.Ignore());
        CreateMap<UpdateVeterinarioRequest, Veterinario>();

        // Mascota
        CreateMap<Mascota, MascotaResponse>();
        CreateMap<CreateMascotaRequest, Mascota>();
        CreateMap<UpdateMascotaRequest, Mascota>();

        // Servicio
        CreateMap<Servicio, ServicioResponse>();
        CreateMap<CreateServicioRequest, Servicio>();
        CreateMap<UpdateServicioRequest, Servicio>();

        // Medicamento
        CreateMap<Medicamento, MedicamentoResponse>();
        CreateMap<CreateMedicamentoRequest, Medicamento>();
        CreateMap<UpdateMedicamentoRequest, Medicamento>();

        // Prescripcion
        CreateMap<Prescripcion, PrescripcionResponse>()
            .ForMember(d => d.NombreMedicamento, opt => opt.Ignore());

        // Pago
        CreateMap<Pago, PagoResponse>();
    }
}
