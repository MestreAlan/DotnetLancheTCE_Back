
using LancheTCE_Back.DTOs;
using LancheTCE_Back.models;
using AutoMapper;
using LancheTCE_Back.DTOs.ProdutoDTO;

namespace LancheTCE_Back.Mapping;

public class ProdutoDTOMappingProfile : Profile
{
    public ProdutoDTOMappingProfile()
    {

        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Produto, ProdutoPOSTDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTOUpdateRequest>().ReverseMap();
    }
}
