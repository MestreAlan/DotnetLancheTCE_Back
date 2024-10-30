using LancheTCE.Context;
using LancheTCE_Back.models;
using LancheTCE_Back.models.filters;
using LancheTCE_Back.Repositories.Produtos;

namespace LancheTCE_Back.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(string Categoria)
    {
        return GetAll().Where(c => c.Categoria.ToLower().Contains(Categoria.ToLower()));
    }

    public PagedList<Produto> GetProdutos(ProdutoFiltroParameters produtosParameters)
    {
        var produtos = GetAll()
            .OrderBy(p => p.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos,
            produtosParameters.PageNumber, produtosParameters.PageSize);

        return produtosOrdenados;
    }

    public PagedList<Produto> GetProdutosFiltroPreco(ProdutoFiltroPreco produtosFiltroParams)
    {
        var produtos = GetAll().AsQueryable();

        if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
        {
            if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
        }

        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

        return produtosFiltrados;
    }

    public PagedList<Produto> GetProdutosFiltroNome(ProdutoFiltroParameters produtosFilterParameters)
    {
        var produtos = GetAll().AsQueryable();

        produtos = produtos.Where(p => p.Nome.ToLower().Contains(produtosFilterParameters.Nome.ToLower()));

        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFilterParameters.PageNumber, produtosFilterParameters.PageSize);

        return produtosFiltrados;
    }
}