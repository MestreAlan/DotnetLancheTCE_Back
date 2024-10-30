using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LancheTCE_Back.DTOs.PedidoDTO;
public class PedidoDTO
{
    public int PedidoId { get; set; }

    [Required]
    [StringLength(20)]
    public string? Status { get; set; }
    public int ValorTotal { get; set; }

    [ForeignKey("UsuarioVendedor")]
    public int IdUsuarioVendedor { get; set; }

    [ForeignKey("UsuarioCliente")]
    public int IdUsuarioCliente { get; set; }

}