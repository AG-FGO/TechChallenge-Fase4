using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Data.Data;
using TechChallenge_Domain.Entities;
using TechChallenge_Domain.Interfaces;

namespace TechChallenge_Data.Repositories
{
    public class ComumRepository<T> : IComumRepository<T> where T : Comum
    {
        protected ApplicationDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public ComumRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _dbSet.Update(entidade);
            _dbContext.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
            _dbContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            _dbSet.Remove(ObterPorId(id));
            _dbContext.SaveChanges();
        }

        public T ObterPorId(int id)
        {
            return _dbSet.Find(id);
        }

        public IList<T> ObterTodos()
        {
            return _dbSet.ToList();
        }
    }
}
