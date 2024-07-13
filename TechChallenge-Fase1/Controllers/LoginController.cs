using Microsoft.AspNetCore.Mvc;
using TechChallenge_Application.Interfaces;
using TechChallenge_Application.Requests.Usuario;
using TechChallenge_Fase1.Services;

namespace TechChallenge_Fase1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUsuarioUseCase _usuarioUseCase;

        public LoginController(ITokenService tokenService, IUsuarioUseCase usuarioUseCase)
        {
            this._tokenService = tokenService;
            this._usuarioUseCase = usuarioUseCase;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var usuarioCadastrado = _usuarioUseCase.ObterPorNomeESenha(request.Nome, request.Senha);

            if (usuarioCadastrado == null)
                return Unauthorized(new {mensagem = "Usuário ou senha inválidos"});

            var token = _tokenService.GenerateToken(usuarioCadastrado);

            return Ok(new { token });
        }



    }
}
