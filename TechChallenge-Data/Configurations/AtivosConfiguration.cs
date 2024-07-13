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
    public class AtivosConfiguration : IEntityTypeConfiguration<Ativos>
    {
        public void Configure(EntityTypeBuilder<Ativos> builder)
        {
            builder.ToTable("Ativos");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(a => a.IdCarteira).HasColumnType("INT").IsRequired();
            builder.Property(a => a.Quantidade).HasColumnType("INT").IsRequired();
            builder.Property(a => a.DataCompra).HasColumnType("DATETIME").IsRequired();
            builder.HasOne(a => a.Acao);
            builder.HasOne(c => c.Carteira).WithMany(c => c.Acoes).HasForeignKey(a => a.IdCarteira);

        }
    }
}
