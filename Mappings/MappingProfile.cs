// ═══════════════════════════════════════════════════════
// ARCHIVO: MappingProfile.cs
// QUÉ HACE: Define las reglas de conversión automática entre entidades y DTOs.
//           AutoMapper usa este perfil para transformar objetos sin escribir código
//           de conversión manual en cada servicio o repositorio.
//           Solo se registran los mapeos que AutoMapper NO puede resolver por convención
//           (nombres de propiedades distintos, campos calculados, campos que se ignoran).
// QUIÉN LO USA: Se registra en Program.cs con AddAutoMapper() y se inyecta en los Services
// ═══════════════════════════════════════════════════════

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
        // ─── Cliente ────────────────────────────────────────────
        // Mapeos directos — los nombres de propiedades coinciden exactamente
        CreateMap<Cliente, ClienteResponse>();
        CreateMap<CreateClienteRequest, Cliente>();
        CreateMap<UpdateClienteRequest, Cliente>();

        // ─── Veterinario ────────────────────────────────────────
        CreateMap<Veterinario, VeterinarioResponse>();
        CreateMap<CreateVeterinarioRequest, Veterinario>()
            // IdUsuario lo asigna el SP al crear la cuenta — AutoMapper no debe tocarlo
            .ForMember(d => d.IdUsuario, opt => opt.Ignore())
            // FechaAlta la asigna SQL Server con DEFAULT GETDATE() — no viene del request
            .ForMember(d => d.FechaAlta, opt => opt.Ignore())
            // Activo siempre es true al crear — no se expone en el request
            .ForMember(d => d.Activo, opt => opt.Ignore());
        CreateMap<UpdateVeterinarioRequest, Veterinario>();

        // ─── Mascota ────────────────────────────────────────────
        CreateMap<Mascota, MascotaResponse>();
        CreateMap<CreateMascotaRequest, Mascota>();
        CreateMap<UpdateMascotaRequest, Mascota>();

        // ─── Servicio ───────────────────────────────────────────
        CreateMap<Servicio, ServicioResponse>();
        CreateMap<CreateServicioRequest, Servicio>();
        CreateMap<UpdateServicioRequest, Servicio>();

        // ─── Medicamento ────────────────────────────────────────
        CreateMap<Medicamento, MedicamentoResponse>();
        CreateMap<CreateMedicamentoRequest, Medicamento>();
        CreateMap<UpdateMedicamentoRequest, Medicamento>();

        // ─── Prescripcion ───────────────────────────────────────
        CreateMap<Prescripcion, PrescripcionResponse>()
            // NombreMedicamento no está en la entidad Prescripcion — viene del SP (JOIN con MEDICAMENTO).
            // El repositorio lo asigna manualmente después de la consulta.
            .ForMember(d => d.NombreMedicamento, opt => opt.Ignore());

        // ─── Pago ───────────────────────────────────────────────
        CreateMap<Pago, PagoResponse>();
    }
}
