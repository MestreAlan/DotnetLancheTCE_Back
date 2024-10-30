using LancheTCE_Back.models;
using AutoMapper;

namespace LancheTCE_Back.Mapping;

public class UsuarioDTOMappingProfile : Profile
{
  public UsuarioDTOMappingProfile()
  {

    CreateMap<Usuario, UsuarioGETDTO>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));

    CreateMap<UsuarioPOSTDTO, Usuario>()
        .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));

    CreateMap<UsuarioPUTDTO, Usuario>()
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco != null ? src.Endereco : null));
  }
}
