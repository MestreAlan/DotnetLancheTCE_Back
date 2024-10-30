using LancheTCE_Back.Repositories.Produtos;


namespace LancheTCE_Back.Repositories;

public interface IUnitOfWork
{
    IProdutoRepository ProdutoRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
    IPedidoRepository PedidoRepository { get; }
    IProdutoPedidoRepository ProdutoPedidoRepository { get; }
    IEnderecoRepository EnderecoRepository { get; }
    void Commit();
}
