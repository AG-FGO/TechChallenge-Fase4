using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge_Application.DTOs
{
    public class CarteiraDTO : ComumDTO
    {
        public int UsuarioId { get; set; }
        public float Saldo { get; set; }
        public List<AtivosDTO>? Acoes { get; set; }

        public UsuarioDTO Usuario { get; set; }

        public CarteiraDTO()
        {
            Acoes = new List<AtivosDTO>();
        }
        public CarteiraDTO(int id, int usuarioId, float saldo)
        {
            Id = id;
            UsuarioId = usuarioId;
            Saldo = saldo;

        }
    }
}
