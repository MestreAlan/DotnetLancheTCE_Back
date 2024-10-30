using APICatalogo.DTOs;
namespace LancheTCE_Back.models.filters
{
    public class PedidoFiltroParameters : PaginationParameters
    {
        public string? Status { get; set; }
        public int ValorTotal { get; set; }
    }
}