using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatalogo.DTOs;

namespace LancheTCE_Back.models.filters
{
    public class PedidoFiltroValor : PaginationParameters
    {
        public int? ValorTotal { get; set; }
        public string ValorCriterio { get; set;}
    }
}