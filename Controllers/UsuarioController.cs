using LancheTCE_Back.models;
using LancheTCE_Back.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LancheTCE_Back.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UsuarioController(IUnitOfWork uof, IMapper mapper, IConfiguration configuration)
    {
      _uof = uof;
      _mapper = mapper;
      _configuration = configuration;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UsuarioGETDTO>> Get()
    {
      var usuarios = _uof.UsuarioRepository.GetUsuarios(new UserParameters());

      if (usuarios == null)
        return NotFound();

      var usuariosDto = _mapper.Map<IEnumerable<UsuarioGETDTO>>(usuarios);

      return Ok(usuariosDto);
    }

    [HttpGet("{id}", Name = "ObterUsuario")]
    public ActionResult<UsuarioGETDTO> Get(int id)
    {
      var usuario = _uof.UsuarioRepository.GetUsuarioComEndereco(id);

      if (usuario == null)
        return NotFound("Usuário não encontrado...");

      var usuarioDto = _mapper.Map<UsuarioGETDTO>(usuario);

      return Ok(usuarioDto);
    }

    [HttpPost]
    public ActionResult<UsuarioGETDTO> Post(UsuarioPOSTDTO usuarioDto)
    {
      if (usuarioDto == null)
        return BadRequest();

      var usuario = _mapper.Map<Usuario>(usuarioDto);

      var novoUsuario = _uof.UsuarioRepository.Create(usuario);
      _uof.Commit();

      var novoUsuarioDto = _mapper.Map<UsuarioGETDTO>(novoUsuario);

      return new CreatedAtRouteResult("ObterUsuario",
          new { id = novoUsuarioDto.UsuarioId }, novoUsuarioDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO loginDto)
    {
      if (loginDto == null)
        return BadRequest("Dados de login inválidos.");

      var usuario = await _uof.UsuarioRepository.GetUsuarioPorEmail(loginDto.Email);

      if (usuario == null || usuario.Senha != loginDto.Senha)
        return Unauthorized("Email ou senha inválidos.");

      var token = GenerateJwtToken(usuario);

      var loginResponse = new LoginResponseDTO
      {
        Token = token,
        Nome = usuario.Nome,
        Email = usuario.Email
      };

      return Ok(loginResponse);
    }

    private string GenerateJwtToken(Usuario usuario)
    {
      var claims = new[]
      {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          issuer: _configuration["Jwt:Issuer"],
          audience: _configuration["Jwt:Audience"],
          claims: claims,
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: creds);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPut("{id:int}")]
    public ActionResult<UsuarioGETDTO> Put(int id, UsuarioPUTDTO usuarioDto)
    {
      if (id != usuarioDto.UsuarioId)
        return BadRequest();

      var usuario = _uof.UsuarioRepository.GetUsuarioComEndereco(id);

      if (usuario == null)
        return NotFound("Usuário não encontrado...");

      usuario.Nome = usuarioDto.Nome;
      usuario.Email = usuarioDto.Email;
      usuario.Senha = usuarioDto.Senha;
      usuario.Perfil = usuarioDto.Perfil;
      usuario.Contato = usuarioDto.Contato;

      if (usuarioDto.Endereco == null)
      {
        return BadRequest("O endereço não pode ser nulo ao atualizar um usuário.");
      }

      if (usuario.Endereco.EnderecoId == usuarioDto.Endereco.EnderecoId)
      {
        usuario.Endereco.Andar = usuarioDto.Endereco.Andar;
        usuario.Endereco.Sala = usuarioDto.Endereco.Sala;
        usuario.Endereco.Departamento = usuarioDto.Endereco.Departamento;
      }
      else if (usuario.Endereco == null)
      {
        var enderecoExistente = _uof.EnderecoRepository.Get(e => e.EnderecoId == usuarioDto.Endereco.EnderecoId);
        if (enderecoExistente != null)
        {
          return BadRequest("Não é possível atualizar um endereço que já pertence a outro usuário.");
        }

        var endereco = _mapper.Map<Endereco>(usuarioDto.Endereco);
        var enderecoAtualizado = _uof.UsuarioRepository.CreateOrUpdateEndereco(endereco);
        usuario.Endereco = enderecoAtualizado;
      }
      else
      {
        return BadRequest("Não é possível criar um endereço pois já existe um endereço cadastrado para este usuário.");
      }

      var usuarioAtualizado = _uof.UsuarioRepository.Update(usuario);
      _uof.Commit();

      var usuarioAtualizadoDto = _mapper.Map<UsuarioGETDTO>(usuarioAtualizado);

      return Ok(usuarioAtualizadoDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<UsuarioGETDTO> Delete(int id)
    {
      var usuario = _uof.UsuarioRepository.Get(u => u.UsuarioId == id);

      if (usuario == null)
        return NotFound("Usuário não encontrado...");

      var usuarioDeletado = _uof.UsuarioRepository.Delete(usuario);
      _uof.Commit();

      var usuarioDeletadoDto = _mapper.Map<UsuarioGETDTO>(usuarioDeletado);

      return Ok(usuarioDeletadoDto);
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<UsuarioGETDTO>> Get([FromQuery] UserParameters userParameters)
    {
      var usuarios = _uof.UsuarioRepository.GetUsuarios(userParameters);

      var metadata = new
      {
        usuarios.TotalCount,
        usuarios.PageSize,
        usuarios.CurrentPage,
        usuarios.TotalPages,
        usuarios.HasNext,
        usuarios.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

      var usuariosDTO = _mapper.Map<IEnumerable<UsuarioGETDTO>>(usuarios);

      return Ok(usuariosDTO);
    }

    [HttpGet("filter/nome/pagination")]
    public ActionResult<IEnumerable<UsuarioGETDTO>> GetUsuariosFilter([FromQuery] UsuarioFiltroParameters usuarioFiltroParameters)
    {
      var usuarios = _uof.UsuarioRepository.GetUsuariosFiltro(usuarioFiltroParameters);

      var metadata = new
      {
        usuarios.TotalCount,
        usuarios.PageSize,
        usuarios.CurrentPage,
        usuarios.TotalPages,
        usuarios.HasNext,
        usuarios.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

      var usuariosDTO = _mapper.Map<IEnumerable<UsuarioGETDTO>>(usuarios);

      return Ok(usuariosDTO);
    }
  }
}
