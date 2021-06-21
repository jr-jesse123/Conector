﻿// <auto-generated />
using System;
using InfraEstrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InfraEstrutura.Migrations
{
    [DbContext(typeof(AlmocharifadoContext))]
    [Migration("20210620202222_corre")]
    partial class corre
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Alocacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContratoLocacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataAlocacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResponsavelCPF")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ResponsavelCPF");

                    b.ToTable("Alocaoes");
                });

            modelBuilder.Entity("Entities.Devolucao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlocacaoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("FerramentaPatrimonio")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Observacoe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlocacaoId");

                    b.HasIndex("FerramentaPatrimonio");

                    b.ToTable("Devolucao");
                });

            modelBuilder.Entity("Entities.Ferramenta", b =>
                {
                    b.Property<string>("Patrimonio")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("AlocacaoId")
                        .HasColumnType("int");

                    b.Property<bool>("Baixada")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmManutencao")
                        .HasColumnType("bit");

                    b.Property<string>("Fotos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Patrimonio");

                    b.HasIndex("AlocacaoId");

                    b.ToTable("Ferramentas");
                });

            modelBuilder.Entity("Entities.Funcionario", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Cargo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CPF");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("Entities.Alocacao", b =>
                {
                    b.HasOne("Entities.Funcionario", "Responsavel")
                        .WithMany()
                        .HasForeignKey("ResponsavelCPF");

                    b.Navigation("Responsavel");
                });

            modelBuilder.Entity("Entities.Devolucao", b =>
                {
                    b.HasOne("Entities.Alocacao", null)
                        .WithMany("Devolucoes")
                        .HasForeignKey("AlocacaoId");

                    b.HasOne("Entities.Ferramenta", "Ferramenta")
                        .WithMany()
                        .HasForeignKey("FerramentaPatrimonio");

                    b.Navigation("Ferramenta");
                });

            modelBuilder.Entity("Entities.Ferramenta", b =>
                {
                    b.HasOne("Entities.Alocacao", null)
                        .WithMany("Ferramentas")
                        .HasForeignKey("AlocacaoId");
                });

            modelBuilder.Entity("Entities.Alocacao", b =>
                {
                    b.Navigation("Devolucoes");

                    b.Navigation("Ferramentas");
                });
#pragma warning restore 612, 618
        }
    }
}
