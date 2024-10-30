using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
    [Table("Pedidos_Produtos")]
    public class ProdutoPedido
    {
        [Key]
        public int Pedido_ProdutoId { get; set; }
        public int Quantidade { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        public Produto? Produto { get; set; }

        [ForeignKey("Pedido")]
        public int IdPedido { get; set; }
        public Pedido? Pedido { get; set; }
    }
}
