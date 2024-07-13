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
    public class AcaoConfiguration : IEntityTypeConfiguration<Acao>
    {
        public void Configure(EntityTypeBuilder<Acao> builder)
        {
            builder.ToTable("Acao");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(a => a.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(a => a.Valor).HasColumnType("DECIMAL(18,2)").IsRequired();
        }
    }
}
