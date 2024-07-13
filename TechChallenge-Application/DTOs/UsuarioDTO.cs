using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Domain.Entities;

namespace TechChallenge_Application.DTOs
{
    public class UsuarioDTO : ComumDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public TipoPermissao Permissao { get; set; }
        public CarteiraDTO Carteira { get; set; }

        public UsuarioDTO() { }
    }
}
