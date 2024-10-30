using LancheTCE_Back.models;
using AutoMapper;
using LancheTCE_Back.DTOs.PedidoDTO;
using LancheTCE_Back.DTOs.ProdutoDTO;

namespace LancheTCE_Back.Mapping;

public class PedidoDTOMappingProfile : Profile
{
  public PedidoDTOMappingProfile()
  {
    CreateMap<Pedido, PedidoDTO>().ReverseMap();
    CreateMap<Pedido, PedidoDTOUpdateRequest>().ReverseMap();
  }
}
