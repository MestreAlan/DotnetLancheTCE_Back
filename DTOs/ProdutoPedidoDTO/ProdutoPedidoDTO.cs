using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LancheTCE_Back.DTOs.PedidoDTO;
public class ProdutoPedidoDTO
{
    public int Pedido_ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public int IdProduto { get; set; }
    public int IdPedido { get; set; }

}