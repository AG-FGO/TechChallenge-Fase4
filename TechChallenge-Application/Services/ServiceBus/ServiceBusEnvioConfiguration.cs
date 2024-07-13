using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Application.Interfaces;
using TechChallenge_Application.Requests.Acao;
using TechChallenge_Application.Requests.Carteira;

namespace TechChallenge_Application.Services.ServiceBus
{
    public class ServiceBusEnvioConfiguration : IServiceBusUseCase
    {
        private readonly IConfiguration _configuration;
        private readonly IBus _bus;

        public ServiceBusEnvioConfiguration(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        public async Task ComprarAcoes(ComprarAcoesServiceBusRequest message)
        {
            try
            {
                var nomeFila = _configuration.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EnviarMensagem(CompraAcoesRequest message)
        {
            try
            {
                var nomeFila = _configuration.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
