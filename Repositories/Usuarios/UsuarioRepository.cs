using LancheTCE.Context;
using LancheTCE_Back.models;
using Microsoft.EntityFrameworkCore;

namespace LancheTCE_Back.Repositories
{
  public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
  {
    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Usuario> GetUsuarios(UserParameters userParameters)
    {
      var query = _context.Usuarios.Include(u => u.Endereco).AsQueryable();

      if (!string.IsNullOrWhiteSpace(userParameters.Nome))
      {
        query = query.Where(u => u.Nome.Contains(userParameters.Nome));
      }

      if (!string.IsNullOrWhiteSpace(userParameters.Email))
      {
        query = query.Where(u => u.Email.Contains(userParameters.Email));
      }

      return PagedList<Usuario>.ToPagedList(query,
          userParameters.PageNumber,
          userParameters.PageSize);
    }

    public PagedList<Usuario> GetUsuariosFiltro(UsuarioFiltroParameters usuarioFiltroParameters)
    {
      var query = _context.Usuarios.Include(u => u.Endereco).AsQueryable();

      if (!string.IsNullOrWhiteSpace(usuarioFiltroParameters.Nome))
      {
        query = query.Where(u => u.Nome.Contains(usuarioFiltroParameters.Nome));
      }

      if (!string.IsNullOrWhiteSpace(usuarioFiltroParameters.Email))
      {
        query = query.Where(u => u.Email.Contains(usuarioFiltroParameters.Email));
      }

      return PagedList<Usuario>.ToPagedList(query,
          usuarioFiltroParameters.PageNumber,
          usuarioFiltroParameters.PageSize);
    }

    public Endereco CreateOrUpdateEndereco(Endereco endereco)
    {
      var enderecoExistente = _context.Enderecos
          .FirstOrDefault(e => e.EnderecoId == endereco.EnderecoId);

      if (enderecoExistente == null)
      {
        _context.Enderecos.Add(endereco);
      }
      else
      {
        enderecoExistente.Andar = endereco.Andar;
        enderecoExistente.Sala = endereco.Sala;
        enderecoExistente.Departamento = endereco.Departamento;
        _context.Enderecos.Update(enderecoExistente);
        endereco = enderecoExistente;
      }

      _context.SaveChanges();
      return endereco;
    }

    public Usuario GetUsuarioComEndereco(int id)
    {
      return _context.Usuarios.Include(u => u.Endereco).FirstOrDefault(u => u.UsuarioId == id);
    }

    public async Task<Usuario> GetUsuarioPorEmail(string email)
    {
      return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }
  }
}
