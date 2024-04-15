using AutoMapper;
using Models.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            

            CreateMap<Models.Models.User, UserResponseDto>().ReverseMap()
                .ForMember(x => x.UserId, src => src.MapFrom(p => p.Id))
                .ForMember(x => x.Nombre, src => src.MapFrom(p => p.Nombre))
                .ForMember(x => x.Apellidos, src => src.MapFrom(p => p.Apellidos))
                .ForMember(x => x.NombreUsuario, src => src.MapFrom(p => p.NombreUsuario))
                .ForMember(x => x.Contraseña, src => src.MapFrom(p => p.Contraseña))
                .ForMember(x => x.FechaNacimiento, src => src.MapFrom(p => p.FechaNacimiento))
                .ForMember(x => x.Celular, src => src.MapFrom(p => p.Celular))
                .ForMember(x => x.Estado, src => src.MapFrom(p => p.Estado))
                ;

        }
    }
}
