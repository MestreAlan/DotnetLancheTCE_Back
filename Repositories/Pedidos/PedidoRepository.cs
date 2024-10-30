using LancheTCE.Context;
using LancheTCE_Back.models;
using LancheTCE_Back.models.filters;
using Microsoft.EntityFrameworkCore;

namespace LancheTCE_Back.Repositories;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
  public PedidoRepository(AppDbContext context) : base(context)
  {
  }

  public PagedList<Pedido> GetPedidos(PedidoParameters pedidoParameters)
  {
      var query = _context.Pedidos.Include(s => s.Status).AsQueryable();

      if (!string.IsNullOrWhiteSpace(pedidoParameters.Status)){
        query = query.Where(p => p.Status.Contains(pedidoParameters.Status));
      }

      return PagedList<Pedido>.ToPagedList(query, pedidoParameters.PageNumber, pedidoParameters.PageSize);
  }

  public PagedList<Pedido> GetPedidosFiltroValor(PedidoFiltroValor pedidoFiltroParams)
  {
    var pedidos = GetAll().AsQueryable();

    if (pedidoFiltroParams.ValorTotal.HasValue && !string.IsNullOrEmpty(pedidoFiltroParams.ValorCriterio))
    {
      if (pedidoFiltroParams.ValorCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
      {
        pedidos = pedidos.Where(p => p.ValorTotal > pedidoFiltroParams.ValorTotal.Value).OrderBy (p => p.ValorTotal);
      }
      else if (pedidoFiltroParams.ValorCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
      {
        pedidos = pedidos.Where(p => p.ValorTotal < pedidoFiltroParams.ValorTotal.Value).OrderBy (p => p.ValorTotal);
      }
      else if (pedidoFiltroParams.ValorCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
      {
        pedidos = pedidos.Where(p => p.ValorTotal == pedidoFiltroParams.ValorTotal.Value).OrderBy (p => p.ValorTotal);
      }
    }

    var pedidosFiltrados = PagedList<Pedido>.ToPagedList(pedidos, pedidoFiltroParams.PageNumber, pedidoFiltroParams.PageSize);

    return pedidosFiltrados;
  }

  public PagedList<Pedido> GetPedidosFiltroStatus(PedidoFiltroParameters pedidoFiltroParameters)
  {
    var pedidos = GetAll().AsQueryable();

    pedidos = pedidos.Where(p => p.Status.ToLower().Contains(pedidoFiltroParameters.Status.ToLower()));

    var pedidosFiltrados = PagedList<Pedido>.ToPagedList(pedidos, pedidoFiltroParameters.PageNumber, pedidoFiltroParameters.PageSize);

    return pedidosFiltrados;
  }
}