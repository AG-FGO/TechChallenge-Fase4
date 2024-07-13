using Mapster;
using TechChallenge_Application.DTOs;
using TechChallenge_Application.Interfaces;
using TechChallenge_Application.Requests.Acao;
using TechChallenge_Domain.Interfaces;

namespace TechChallenge_Application.Services.Acao
{
    public class AcaoUseCase : IAcaoUseCase
    {
        private readonly IAcaoRepository _acaoRepository;

        public AcaoUseCase(IAcaoRepository acaoRepository)
        {
            _acaoRepository = acaoRepository;
        }

        public void CadastrarAcao(AcaoRequest acaoRequest)
        {
            try
            {
                var acao = acaoRequest.Adapt<TechChallenge_Domain.Entities.Acao>();
                _acaoRepository.Cadastrar(acao);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AcaoDTO> ListarAcoesDisponiveis()
        {
            try
            {
                var acoes = _acaoRepository.ObterTodos();
                return acoes.Adapt<List<AcaoDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
