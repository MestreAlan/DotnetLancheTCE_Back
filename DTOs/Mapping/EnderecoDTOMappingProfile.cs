using LancheTCE_Back.models;
using AutoMapper;

namespace LancheTCE_Back.Mapping;

public class EnderecoDTOMappingProfile : Profile
{
  public EnderecoDTOMappingProfile()
  {
    CreateMap<Endereco, EnderecoDTO>().ReverseMap();
  }
}
