using AutoMapper;
using LancheTCE_Back.DTOs.PedidoDTO;
using LancheTCE_Back.models;
using LancheTCE_Back.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LancheTCE_Back.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutoPedidosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutoPedidosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoPedido>> Get()
    {
        var produtoPedidos = _uof.ProdutoPedidoRepository.GetAll();
        var produtoPedidosDto = _mapper.Map<IEnumerable<ProdutoPedido>>(produtoPedidos);
        return Ok(produtoPedidosDto);
    }

    [HttpPost]
    public ActionResult<PedidoDTO> AddProdutoAoPedido(int pedidoId, int produtoId)
    {
        var pedido = _uof.ProdutoPedidoRepository.Get(p => p.IdPedido == pedidoId);
        if (pedido is null)
        {
            return NotFound("Pedido não encontrado...");
        }

        var produto = _uof.ProdutoPedidoRepository.Get(p => p.IdProduto == produtoId);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        var produtoPedido = new ProdutoPedido
        {
            IdPedido = pedidoId,
            IdProduto = produtoId,
            Quantidade = 1
        };

        _uof.ProdutoPedidoRepository.Create(produtoPedido);
        var produtoPedidoDTO = _mapper.Map<ProdutoPedidoDTO>(produtoPedido);
        return Ok(produtoPedidoDTO);
    }

    /*[HttpPatch]
    public ActionResult<PedidoDTO> Patch(int pedidoId, int produtoId, [FromBody] JsonPatchDocument<PedidoDTOUpdateRequest> patchPedidoDTO)
    {
        if (patchPedidoDTO is null || pedidoId <= 0 || produtoId <= 0)
            return BadRequest("ERRO: patchPedidoDTO está nulo ou um dos IDs é inválido.");
        
        var pedido = _uof.PedidoRepository.Get(p => p.PedidoId == pedidoId);
        if (pedido is null)
        {
            return NotFound("Pedido não encontrado...");
        }

        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == produtoId);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
    }*/

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoPedidoDTO> Delete(int id)
    {
        var produtoPedido = _uof.ProdutoPedidoRepository.Get(p => p.Pedido_ProdutoId == id);
        if (produtoPedido is null)
            return NotFound("ProdutoPedido não encontrado...");

        var produtoPedidoDeletado = _uof.ProdutoPedidoRepository.Delete(produtoPedido);
        _uof.Commit();

        var produtoPedidoDeletadoDTO = _mapper.Map<ProdutoPedidoDTO>(produtoPedidoDeletado);

        return Ok(produtoPedidoDeletadoDTO);
    }


}