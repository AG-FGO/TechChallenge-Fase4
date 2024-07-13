using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge_Application.DTOs
{
    public class AcaoDTO : ComumDTO
    {
        public string Nome { get; set; }
        public float Valor { get; set; }

        public AcaoDTO() { }
    }
}
