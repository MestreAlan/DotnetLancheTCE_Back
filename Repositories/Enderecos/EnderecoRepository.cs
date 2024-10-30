using LancheTCE.Context;
using LancheTCE_Back.models;

namespace LancheTCE_Back.Repositories
{
  public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
  {
    public EnderecoRepository(AppDbContext context) : base(context)
    {
    }

    public Endereco CreateOrUpdateEndereco(Endereco endereco)
    {
      var enderecoExistente = _context.Enderecos
          .FirstOrDefault(e => e.EnderecoId == endereco.EnderecoId);

      if (enderecoExistente != null)
      {
        enderecoExistente.Andar = endereco.Andar;
        enderecoExistente.Sala = endereco.Sala;
        enderecoExistente.Departamento = endereco.Departamento;
        _context.Enderecos.Update(enderecoExistente);
        return enderecoExistente;
      }

      _context.Enderecos.Add(endereco);
      return endereco;
    }
  }
}
