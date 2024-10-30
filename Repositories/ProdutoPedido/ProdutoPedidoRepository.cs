using LancheTCE.Context;
using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories;

public class ProdutoPedidoRepository : Repository<ProdutoPedido>, IProdutoPedidoRepository
{
  public ProdutoPedidoRepository(AppDbContext context) : base(context)
  {
  }
}