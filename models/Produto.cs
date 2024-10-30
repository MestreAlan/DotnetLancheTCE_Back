using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        public string? Categoria { get; set; }
        public int Quantidade { get; set; }

        public string? ImagemUrl { get; set; }

        [ForeignKey(nameof(UsuarioVendedor))]
        public int IdUsuarioVendedor { get; set; }
        public Usuario? UsuarioVendedor { get; set; }
    }
}
