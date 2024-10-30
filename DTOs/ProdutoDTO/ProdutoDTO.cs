using System.ComponentModel.DataAnnotations;

namespace LancheTCE_Back.DTOs.ProdutoDTO;

public class ProdutoDTO
{
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Required]
    public decimal Preco { get; set; }

    public string? ImagemUrl { get; set; }

    public string? Categoria { get; set; }

    // public int VendedorId{get; set;}
}
