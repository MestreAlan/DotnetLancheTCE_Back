using AutoMapper;
using LancheTCE_Back.DTOs.PedidoDTO;
using LancheTCE_Back.models;
using LancheTCE_Back.models.filters;
using LancheTCE_Back.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LancheTCE_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public PedidosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Pedido>> Get()
    {
        var pedidos = _uof.PedidoRepository.GetAll();
        if (pedidos is null)
            return NotFound("Pedido não encontrado...");

        var pedidosDto = _mapper.Map<IEnumerable<Pedido>>(pedidos);
        return Ok(pedidosDto);
    }

    [HttpGet("{id}", Name = "ObterPedido")]
    public ActionResult<PedidoDTO> Get(int id)
    {
        var pedido = _uof.PedidoRepository.Get(p => p.PedidoId == id);
        if (pedido is null)
            return NotFound("Pedido não encontrado...");

        var pedidoDTO = _mapper.Map<PedidoDTO>(pedido);
        return Ok(pedidoDTO);
    }

    [HttpGet("filter/status/pagination")]
    public ActionResult<IEnumerable<PedidoDTO>> GetPedidoFilterStatus([FromQuery] PedidoFiltroParameters pedidoFiltroParameters)
    {
        var pedidos = _uof.PedidoRepository.GetPedidosFiltroStatus(pedidoFiltroParameters);
        return ObterPedidos(pedidos);
    }

    [HttpGet("filter/valor/pagination")]
    public ActionResult<IEnumerable<PedidoDTO>> GetPedidoFilterValor([FromQuery] PedidoFiltroValor pedidoFiltroValor)
    {
        var pedidos = _uof.PedidoRepository.GetPedidosFiltroValor(pedidoFiltroValor);
        return ObterPedidos(pedidos);
    }

    private ActionResult<IEnumerable<PedidoDTO>> ObterPedidos(PagedList<Pedido> pedidos)
    {
        var metadata = new
        {
            pedidos.TotalCount,
            pedidos.PageSize,
            pedidos.CurrentPage,
            pedidos.TotalPages,
            pedidos.HasNext,
            pedidos.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        var pedidosDTO = _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);

        return Ok(pedidosDTO);
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<PedidoDTO>> Get([FromQuery] PedidoParameters pedidoParameters)
    {
        var pedidos = _uof.PedidoRepository.GetPedidos(pedidoParameters);

        var metadata = new
        {
            pedidos.TotalCount,
            pedidos.PageSize,
            pedidos.CurrentPage,
            pedidos.TotalPages,
            pedidos.HasNext,
            pedidos.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        var pedidosDTO = _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);

        return Ok(pedidosDTO);
    }

    [HttpPost]
    public ActionResult<PedidoDTO> Post(PedidoDTO pedidoDTO)
    {
        if (pedidoDTO is null)
            return BadRequest();

        var pedido = _mapper.Map<Pedido>(pedidoDTO);

        var novoPedido = _uof.PedidoRepository.Create(pedido);
        _uof.Commit();

        var novoPedidoDTO = _mapper.Map<PedidoDTO>(novoPedido);

        return new CreatedAtRouteResult("ObterPedido",
            new { id = novoPedidoDTO.PedidoId }, novoPedidoDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<PedidoDTO> Put(int id, PedidoDTO pedidoDTO)
    {
        if (id != pedidoDTO.PedidoId)
            return BadRequest();
        var pedido = _mapper.Map<Pedido>(pedidoDTO);
        var pedidoAtualizado = _uof.PedidoRepository.Update(pedido);
        _uof.Commit();

        var pedidoAtualizadoDTO = _mapper.Map<PedidoDTO>(pedidoAtualizado);

        return Ok(pedidoAtualizadoDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<PedidoDTO> Delete(int id)
    {
        var pedido = _uof.PedidoRepository.Get(p => p.PedidoId == id);
        if (pedido is null)
            return NotFound("Pedido não encontrado...");

        var pedidoDeletado = _uof.PedidoRepository.Delete(pedido);
        _uof.Commit();

        var pedidoDeletadoDTO = _mapper.Map<PedidoDTO>(pedidoDeletado);

        return Ok(pedidoDeletadoDTO);
    }
}
