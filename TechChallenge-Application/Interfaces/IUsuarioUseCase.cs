using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Application.DTOs;
using TechChallenge_Application.Requests.Usuario;

namespace TechChallenge_Application.Interfaces
{
    public interface IUsuarioUseCase
    {
        UsuarioDTO ObterPorNomeESenha(string nome, string senha);

        UsuarioDTO ObterUsuarioDadosCompletos(int id);

        void CadastroSimples(UsuarioCadastroRequest usuario);

        void DeletarUsuario(int id);
    }
}
