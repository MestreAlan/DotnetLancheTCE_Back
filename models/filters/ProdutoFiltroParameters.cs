

using APICatalogo.DTOs;

namespace LancheTCE_Back.models.filters
{
    public class ProdutoFiltroParameters : PaginationParameters
    {
        public string? Nome {get; set;}
        
    }
}