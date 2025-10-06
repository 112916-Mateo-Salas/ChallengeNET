using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Domicilios;
using UserChallenge.Application.DTOs.Usuarios;
using UserChallenge.Domain.Model;

namespace UserChallenge.Application.Mappings
{
    [ExcludeFromCodeCoverage]
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, RegisterUsuario>()
                .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Domicilio))
                .ReverseMap();

            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Domicilio))
                .ReverseMap();

            CreateMap<Usuario, UpdateUsuario>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Domicilio))
                .ReverseMap();

            CreateMap<Usuario, CreatedUsuarioDto>()
                .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Domicilio))
                .ReverseMap();

            CreateMap<Domicilio, DomicilioDto>().ReverseMap();

            CreateMap<Domicilio, CreatedDomicilioDto>().ReverseMap();
        }
    }
}
