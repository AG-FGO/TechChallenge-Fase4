using TechChallenge_Data.Data;
using TechChallenge_Domain.Entities;
using TechChallenge_Domain.Interfaces;

namespace TechChallenge_Data.Repositories
{
    public class AcaoRepository : ComumRepository<Acao>, IAcaoRepository
    {
        protected ApplicationDbContext _dbContext;

        public AcaoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
