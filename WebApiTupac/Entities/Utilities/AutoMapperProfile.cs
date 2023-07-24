using AutoMapper;
using WebApiTupac.Entities.DTO;

namespace WebApiTupac.Entities.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
        }
    }
}
