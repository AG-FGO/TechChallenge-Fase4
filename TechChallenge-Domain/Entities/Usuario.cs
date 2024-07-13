﻿//using TechChallenge_Fase1.Enums;

namespace TechChallenge_Domain.Entities
{
    public class Usuario : Comum
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public TipoPermissao Permissao { get; set; }
        public Carteira Carteira { get; set; }

        public Usuario() { }
    }
}
