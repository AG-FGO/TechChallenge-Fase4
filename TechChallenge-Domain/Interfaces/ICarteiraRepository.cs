using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Domain.Entities;

namespace TechChallenge_Domain.Interfaces
{
    public interface ICarteiraRepository : IComumRepository<Carteira>
    {
        Carteira GetCarteiraByUsuarioID(int id);
        void AdicionarValorCarteira(int id, float valor);
        bool RemoverValorCarteira(int id, float valor);
        bool ComprarAcoes(int IdUsuario, int IdAcao, int quantidade, bool enviaServiceBus = false);
        bool VenderAcoes(int IdUsuario, int IdAcao, int quantidade);
    }
}
