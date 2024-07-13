using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Application.Requests.Acao;
using TechChallenge_Application.Requests.Carteira;

namespace TechChallenge_Application.Interfaces
{
    public interface IServiceBusUseCase
    {
        Task EnviarMensagem(CompraAcoesRequest message);
        Task ComprarAcoes(ComprarAcoesServiceBusRequest message);
    }
}
