using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge_Application.Interfaces;
using TechChallenge_Application.Requests.Usuario;
using TechChallenge_Fase1.Enums;

namespace TechChallenge_Fase1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly ILogger<UsuarioController> _logger;

        private readonly IUsuarioUseCase _usuarioUseCase;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioUseCase usuarioUseCase)
        {
            _logger = logger;
            _usuarioUseCase = usuarioUseCase;
        }


        /*[Authorize]
        [Authorize(Roles = $"{Permissao.Administrador}")]*/
        [HttpGet]
        [Route(nameof(ObterUsuario))]
        public ActionResult ObterUsuario(int id) 
        {
            try
            {
                _logger.LogInformation($"Buscado usuário por id: {id}");

                var usuario = _usuarioUseCase.ObterUsuarioDadosCompletos(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a busca");
            }
        }

       /* ANTIGO
        * 
        * [HttpPost]
        [Route(nameof(CadastrarUsuario))]
        public ActionResult CadastrarUsuario([FromBody] UsuarioCadastroRequest usuarioRequest)
        {
            try
            {
                _logger.LogInformation($"Cadastrando usuário {usuarioRequest.Nome}");

                _usuarioRepository.CadastroSimples(new Usuario
                {
                    Nome = usuarioRequest.Nome,
                    Senha = usuarioRequest.Senha
                });

                return Ok("Usuario cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante o cadastro");
            }
        }*/

        [HttpPost]
        [Route(nameof(CadastrarUsuario))]
        public ActionResult CadastrarUsuario([FromBody] UsuarioCadastroRequest usuarioRequest)
        {
            try
            {
                _logger.LogInformation($"Cadastrando usuário {usuarioRequest.Nome}");

                _usuarioUseCase.CadastroSimples(usuarioRequest);

                return Ok("Usuario cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante o cadastro");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Administrador}")]
        [HttpDelete]
        [Route(nameof(DeletarUsuario))]
        public IActionResult DeletarUsuario(int id)
        {
            try
            {
                _logger.LogInformation($"Deletando usuário de id: {id}");
                _usuarioUseCase.DeletarUsuario(id);
                return Ok("Usuario deletado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a exclusão");
            }
        }
    }
}
