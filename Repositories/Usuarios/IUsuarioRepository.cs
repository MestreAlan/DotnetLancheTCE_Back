using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories
{
  public interface IUsuarioRepository : IRepository<Usuario>
  {
    PagedList<Usuario> GetUsuarios(UserParameters userParameters);
    PagedList<Usuario> GetUsuariosFiltro(UsuarioFiltroParameters usuarioFiltroParameters);
    Endereco CreateOrUpdateEndereco(Endereco endereco);
    Usuario GetUsuarioComEndereco(int id);
    Task<Usuario> GetUsuarioPorEmail(string email);
  }
}
