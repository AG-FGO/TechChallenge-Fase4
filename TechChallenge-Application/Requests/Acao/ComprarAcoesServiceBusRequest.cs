using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Application.DTOs;

namespace TechChallenge_Application.Requests.Acao
{
    public class ComprarAcoesServiceBusRequest
    {
        public int Quantidade { get; set; }
        public CarteiraDTO Carteira { get; set; }
        public AcaoDTO Acao { get; set; }
    }
}
