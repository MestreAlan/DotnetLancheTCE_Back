using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string? Email { get; set; }

        [Required]
        [StringLength(20)]
        public string? Senha { get; set; }

        [Required]
        [StringLength(20)]
        public string? Perfil { get; set; }

        [StringLength(15)]
        public string? Contato { get; set; }

        [ForeignKey("Endereco")]
        public int? IdEndereco { get; set; }
        public Endereco? Endereco { get; set; }

        public ICollection<Produto>? ProdutosVendidos { get; set; }
        public ICollection<Pedido>? PedidosComoVendedor { get; set; }
        public ICollection<Pedido>? PedidosComoCliente { get; set; }
    }
}
