﻿// <auto-generated />
using Almocherifado.InfraEstrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Almocherifado.InfraEstrutura.Migrations
{
    [DbContext(typeof(AlmocherifadoContext))]
    [Migration("20210117234330_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Almocherifado.core.Funcionario", b =>
                {
                    b.Property<string>("_cpf")
                        .HasColumnType("TEXT");

                    b.HasKey("_cpf");

                    b.ToTable("Funcionarios");
                });
#pragma warning restore 612, 618
        }
    }
}
