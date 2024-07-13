using System.ComponentModel.DataAnnotations;

namespace TechChallenge_Application.Requests.Acao
{
    public class AcaoRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public float Valor { get; set; }
    }
}
