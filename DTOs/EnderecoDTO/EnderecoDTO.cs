using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LancheTCE_Back.models
{
  public class EnderecoDTO
  {
    public int EnderecoId { get; set; }
    public string? Andar { get; set; }
    public string? Sala { get; set; }
    public string? Departamento { get; set; }
  }
}
