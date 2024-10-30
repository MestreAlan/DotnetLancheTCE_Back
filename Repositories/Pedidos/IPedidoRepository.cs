using LancheTCE_Back.models;
using LancheTCE_Back.models.filters;

namespace LancheTCE_Back.Repositories;

public interface IPedidoRepository : IRepository<Pedido>
{
  PagedList<Pedido> GetPedidos(PedidoParameters pedidoParameters);
  PagedList<Pedido> GetPedidosFiltroValor(PedidoFiltroValor pedidoFiltroParams);
  PagedList<Pedido> GetPedidosFiltroStatus(PedidoFiltroParameters pedidoFiltroParameters);
}
