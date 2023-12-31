﻿using AutoMapper;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Entities.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UsuariosDTO, Usuario>().ReverseMap();
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<UsuarioCreacionDTO, Usuario>();
            CreateMap<UsuarioActualizacionDTO, Usuario>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PasswordUpdateDTO, Usuario>();
            CreateMap<CarreraDTO, Carrera>().ReverseMap();
            CreateMap<CarreraCreacionDTO, Carrera>();
            CreateMap<MateriaDTO, Materia>().ReverseMap();
            CreateMap<MateriaCreacionDTO, Materia>();
            CreateMap<CursadaCreacionDTO, Cursada>();
            CreateMap<CursadaActualizacionDTO, Cursada>();
        }
    }
}
