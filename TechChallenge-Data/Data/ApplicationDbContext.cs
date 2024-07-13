using Microsoft.EntityFrameworkCore;
using TechChallenge_Domain.Entities;

namespace TechChallenge_Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        /*private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }*/

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Carteira> Carteira { get; set; }
        public DbSet<Acao> Acao { get; set; }
        public DbSet<Ativos> Ativos { get; set; }
    }
}
