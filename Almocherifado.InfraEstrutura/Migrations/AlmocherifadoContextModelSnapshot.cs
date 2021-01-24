﻿// <auto-generated />
using System;
using Almocherifado.InfraEstrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Almocherifado.InfraEstrutura.Migrations
{
    [DbContext(typeof(AlmocherifadoContext))]
    partial class AlmocherifadoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Almocherifado.core.Emprestimo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Devolucao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Entrega")
                        .HasColumnType("TEXT");

                    b.Property<int>("FerramentaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FuncionarioCPF")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Obra")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FerramentaId");

                    b.HasIndex("FuncionarioCPF");

                    b.ToTable("Emprestimos");
                });

            modelBuilder.Entity("Almocherifado.core.Ferramenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descrição")
                        .HasColumnType("TEXT");

                    b.Property<string>("FotoUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeAbreviado")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ferramentas");
                });

            modelBuilder.Entity("Almocherifado.core.Funcionario", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("CPF");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("Almocherifado.core.Emprestimo", b =>
                {
                    b.HasOne("Almocherifado.core.Ferramenta", "Ferramenta")
                        .WithMany()
                        .HasForeignKey("FerramentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Almocherifado.core.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioCPF")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ferramenta");

                    b.Navigation("Funcionario");
                });
#pragma warning restore 612, 618
        }
    }
}
