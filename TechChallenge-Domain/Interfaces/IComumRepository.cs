using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Domain.Entities;

namespace TechChallenge_Domain.Interfaces
{
    public interface IComumRepository<T> where T : Comum
    {
        IList<T> ObterTodos();
        T ObterPorId(int id);
        void Cadastrar(T comum);
        void Alterar(T comum);
        void Excluir(int id);
    }
}
