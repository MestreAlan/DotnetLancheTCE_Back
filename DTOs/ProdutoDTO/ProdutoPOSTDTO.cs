using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LancheTCE_Back.DTOs.ProdutoDTO
{
    public class ProdutoPOSTDTO
    {
        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public string? ImagemUrl { get; set; }

        public string? Quantidade { get; set; }

        public string? Categoria { get; set; }

        public int IdUsuarioVendedor{get; set;}
    }
}