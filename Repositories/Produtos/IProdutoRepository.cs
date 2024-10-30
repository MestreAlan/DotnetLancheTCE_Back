using LancheTCE_Back.models;
using LancheTCE_Back.models.filters;

namespace LancheTCE_Back.Repositories.Produtos;

public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorCategoria(string Categoria);

    PagedList<Produto> GetProdutos(ProdutoFiltroParameters produtosParams);

    PagedList<Produto> GetProdutosFiltroPreco(ProdutoFiltroPreco produtosFiltroParams);
    PagedList<Produto> GetProdutosFiltroNome(ProdutoFiltroParameters produtosFilterParameters);
}
