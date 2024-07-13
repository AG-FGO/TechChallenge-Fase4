using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Domain.Entities;

namespace TechChallenge_Domain.Interfaces
{
    public interface IUsuarioRepository : IComumRepository<Usuario>
    {
        Usuario ObterPorNomeESenha(string nome, string senha);

        Usuario ObterUsuarioDadosCompletos(int id);

        void CadastroSimples(Usuario usuario);
    }
}
