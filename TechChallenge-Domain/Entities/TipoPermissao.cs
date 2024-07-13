using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge_Domain.Entities
{
    public enum TipoPermissao
    {
        Administrador = 1,
        UsuarioComum = 2
    }

    public static class Permissao
    {
        public const string Administrador = "Administrador";
        public const string UsuarioComum = "UsuarioComum";
    }
}
