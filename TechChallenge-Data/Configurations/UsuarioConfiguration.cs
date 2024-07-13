using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TechChallenge_Domain.Entities;

namespace TechChallenge_Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(u => u.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.Permissao).HasConversion<int>().IsRequired();
            builder.Property(u => u.Senha).HasColumnType("VARCHAR(100)").IsRequired();

            builder.HasOne(u => u.Carteira).WithOne(c => c.Usuario).HasForeignKey<Carteira>(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
