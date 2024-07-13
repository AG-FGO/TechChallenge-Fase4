using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Application.DTOs;

namespace TechChallenge_Application.Requests.Usuario
{
    public class UsuarioRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        //public TipoPermissao Permissao { get; set; }
        public CarteiraDTO Carteira { get; set; }

        public UsuarioRequest() { }
    }
}
