using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatalogo.DTOs;

namespace LancheTCE_Back.models.filters
{
    public class ProdutoFiltroPreco : PaginationParameters
    {
        public decimal? Preco {get; set;}

        public string? PrecoCriterio {get; set;}
    }
}