using Mapster;
using TechChallenge_Application.DTOs;
using TechChallenge_Application.Interfaces;
using TechChallenge_Application.Requests.Acao;
using TechChallenge_Domain.Entities;
using TechChallenge_Domain.Interfaces;

namespace TechChallenge_Application.Services.Carteira
{
    public class CarteiraUseCase : ICarteiraUseCase
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IServiceBusUseCase _serviceBusUseCase;
        private readonly IAcaoRepository _acaoRepository;
        public CarteiraUseCase(ICarteiraRepository carteiraRepository, IServiceBusUseCase serviceBusUseCase, IAcaoRepository acaoRepository)
        {
            _carteiraRepository = carteiraRepository;
            _serviceBusUseCase = serviceBusUseCase;
            _acaoRepository = acaoRepository;
        }

        public void AdicionarValorCarteira(int id, float valor)
        {
            try
            {
                _carteiraRepository.AdicionarValorCarteira(id, valor);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ComprarAcoes(int IdUsuario, int IdAcao, int quantidade, bool enviaServiceBus = false)
        {
            try
            {
                if(!enviaServiceBus)
                    return _carteiraRepository.ComprarAcoes(IdUsuario, IdAcao, quantidade, enviaServiceBus);
                else
                {
                    var carteira = _carteiraRepository.GetCarteiraByUsuarioID(IdUsuario);

                    if (carteira == null)
                        return false;

                    var acao = _acaoRepository.ObterPorId(IdAcao);

                    if (acao == null)
                        return false;

                    _serviceBusUseCase.ComprarAcoes(new ComprarAcoesServiceBusRequest
                    {
                        Quantidade = quantidade,
                        Carteira = carteira.Adapt<CarteiraDTO>(),
                        Acao = acao.Adapt<AcaoDTO>()
                    });

                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public CarteiraDTO GetCarteiraByUsuarioID(int id)
        {
            try
            {
                var carteira = _carteiraRepository.GetCarteiraByUsuarioID(id);
                return carteira.Adapt<CarteiraDTO>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool RemoverValorCarteira(int id, float valor)
        {
            try
            {
                return _carteiraRepository.RemoverValorCarteira(id, valor);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool VenderAcoes(int IdUsuario, int IdAcao, int quantidade)
        {
            try
            {
                return _carteiraRepository.VenderAcoes(IdUsuario, IdAcao, quantidade);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
