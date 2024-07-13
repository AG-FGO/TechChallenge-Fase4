using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Application.DTOs;
using TechChallenge_Application.Requests.Acao;

namespace TechChallenge_Application.Interfaces
{
    public interface IAcaoUseCase
    {
        List<AcaoDTO> ListarAcoesDisponiveis();
        void CadastrarAcao(AcaoRequest acaoRequest);
    }
}
