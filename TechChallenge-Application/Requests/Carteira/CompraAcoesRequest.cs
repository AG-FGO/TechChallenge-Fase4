using System.ComponentModel.DataAnnotations;

namespace TechChallenge_Application.Requests.Carteira
{
    public class CompraAcoesRequest
    {
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdAcao { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public bool EnviaServiceBus { get; set; }
    }
}
