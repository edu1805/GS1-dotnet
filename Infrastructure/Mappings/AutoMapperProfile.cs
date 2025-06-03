using AutoMapper;
using SOS_Natureza.Domain.Entities;
using SOS_Natureza.Infrastructure.DTOs;

namespace SOS_Natureza.Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Denuncia, DenunciaDTO>().ReverseMap();
        }
    }
}
