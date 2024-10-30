using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories
{
  public interface IEnderecoRepository : IRepository<Endereco>
  {
    Endereco CreateOrUpdateEndereco(Endereco endereco);
  }

}
