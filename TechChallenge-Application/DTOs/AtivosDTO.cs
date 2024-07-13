using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge_Application.DTOs
{
    public class AtivosDTO : ComumDTO
    {
        public int IdCarteira { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCompra { get; set; }
        public AcaoDTO Acao { get; set; }
        public CarteiraDTO Carteira { get; set; }

        public AtivosDTO()
        {
            Acao = new AcaoDTO();
        }
    }
}
