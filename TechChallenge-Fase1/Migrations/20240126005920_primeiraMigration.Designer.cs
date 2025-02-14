﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TechChallenge_Data.Data;

#nullable disable

namespace TechChallenge_Fase1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240126005920_primeiraMigration")]
    partial class primeiraMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TechChallenge_Fase1.Model.Acao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("DECIMAL(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Acao", (string)null);
                });

            modelBuilder.Entity("TechChallenge_Fase1.Model.Ativos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AcaoId")
                        .HasColumnType("INT");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("DATETIME");

                    b.Property<int>("IdCarteira")
                        .HasColumnType("INT");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("AcaoId");

                    b.HasIndex("IdCarteira");

                    b.ToTable("Ativos", (string)null);
                });

            modelBuilder.Entity("TechChallenge_Fase1.Model.Carteira", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Saldo")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Carteira", (string)null);
                });

            modelBuilder.Entity("TechChallenge_Fase1.Model.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("Permissao")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("TechChallenge_Fase1.Model.Ativos", b =>
                {
                    b.HasOne("TechChallenge_Fase1.Model.Acao", "Acao")
                        .WithMany()
                        .HasForeignKey("AcaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechChallenge_Fase1.Model.Carteira", "Carteira")
                        .WithMany("Acoes")
                        .HasForeignKey("IdCarteira")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acao");

                    b.Navigation("Carteira");
                });

            modelBuilder.Entity("TechChallenge_Fase1.Model.Carteira", b =>
                {
                    b.HasOne("TechChallenge_Fase1.Model.Usuario", "Usuario")
                        .WithOne("Carteira")
                        .HasForeignKey("TechChallenge_Fase1.Model.Carteira", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("TechChallenge_Fase1.Model.Carteira", b =>
                {
                    b.Navigation("Acoes");
                });

            modelBuilder.Entity("TechChallenge_Fase1.Model.Usuario", b =>
                {
                    b.Navigation("Carteira")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
