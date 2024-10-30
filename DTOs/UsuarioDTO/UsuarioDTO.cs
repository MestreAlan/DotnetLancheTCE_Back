using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
  public class UsuarioDTO
  {
    public int UsuarioId { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public string? Perfil { get; set; }
    public string? Contato { get; set; }
  }
}
