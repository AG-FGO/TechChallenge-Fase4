using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Domain.Entities;

namespace TechChallenge_Data.Configurations
{
    public class CarteiraConfiguration : IEntityTypeConfiguration<Carteira>
    {
        public void Configure(EntityTypeBuilder<Carteira> builder)
        {
            builder.ToTable("Carteira");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(c => c.Saldo).HasColumnType("DECIMAL(18,2)").IsRequired();
            builder.HasOne(c => c.Usuario).WithOne(u => u.Carteira).HasForeignKey<Carteira>(c => c.UsuarioId);
            builder.HasMany(c => c.Acoes).WithOne(a => a.Carteira).HasForeignKey(a => a.IdCarteira);

        }
    }
}
