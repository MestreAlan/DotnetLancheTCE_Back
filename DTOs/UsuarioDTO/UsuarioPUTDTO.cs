namespace LancheTCE_Back.models
{
  public class UsuarioPUTDTO
  {
    public int UsuarioId { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public string? Perfil { get; set; }
    public string? Contato { get; set; }
    public EnderecoDTO? Endereco { get; set; }
  }
}
