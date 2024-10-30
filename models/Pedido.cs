using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }

        [Required]
        [StringLength(20)]
        public string? Status { get; set; }
        public int ValorTotal { get; set; }

        [ForeignKey("UsuarioVendedor")]
        public int IdUsuarioVendedor { get; set; }
        public Usuario? UsuarioVendedor { get; set; }

        [ForeignKey("UsuarioCliente")]
        public int IdUsuarioCliente { get; set; }
        public Usuario? UsuarioCliente { get; set; }

        public ICollection<ProdutoPedido>? PedidosProdutos { get; set; }
    }
}
