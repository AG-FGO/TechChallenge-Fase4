﻿namespace TechChallenge_Domain.Entities
{
    public class Ativos : Comum
    {
        public int IdCarteira { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCompra { get; set; }
        public Acao Acao { get; set; }
        public Carteira Carteira { get; set; }

        public Ativos()
        {
            Acao = new Acao();
        }
    }
}
