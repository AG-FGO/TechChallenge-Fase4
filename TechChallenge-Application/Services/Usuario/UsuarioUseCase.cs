using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Application.DTOs;
using TechChallenge_Application.Interfaces;
using TechChallenge_Application.Requests.Usuario;
using TechChallenge_Domain.Entities;
using TechChallenge_Domain.Interfaces;

namespace TechChallenge_Application.Services.Usuario
{
    public class UsuarioUseCase : IUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void CadastroSimples(UsuarioCadastroRequest usuario)
        {
            try
            {
                // _logger.LogInformation($"Cadastrando usuário {usuarioRequest.Nome}");
                _usuarioRepository.CadastroSimples(new TechChallenge_Domain.Entities.Usuario
                {
                    Nome = usuario.Nome,
                    Senha = usuario.Senha
                });
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Exception: {ex.Message}");
                //return BadRequest("Houve um erro durante o cadastro");
            }
        }

        public void DeletarUsuario(int id)
        {
            try
            {
                _usuarioRepository.Excluir(id);
            }
            catch (Exception ex)
            {

            }
        }

        public UsuarioDTO ObterPorNomeESenha(string nome, string senha)
        {
            try
            {
                var usuario = _usuarioRepository.ObterPorNomeESenha(nome, senha);
                return usuario.Adapt<UsuarioDTO>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UsuarioDTO ObterUsuarioDadosCompletos(int id)
        {
            try
            {
                var usuario = _usuarioRepository.ObterUsuarioDadosCompletos(id);
                var retorno = usuario.Adapt<UsuarioDTO>();

                return retorno;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
