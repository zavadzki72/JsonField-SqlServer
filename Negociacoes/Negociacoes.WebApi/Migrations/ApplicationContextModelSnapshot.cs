﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Negociacoes.WebApi.Infra;

#nullable disable

namespace Negociacoes.WebApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.Jogador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nome");

                    b.Property<decimal>("Salario")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("salario");

                    b.HasKey("Id")
                        .HasName("pk_jogador");

                    b.ToTable("jogador", (string)null);
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.NegociacaoJogador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataContratoProposta")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_contrato_proposta");

                    b.Property<DateTime>("DataEvento")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_evento");

                    b.Property<int>("IdTimeDestino")
                        .HasColumnType("int")
                        .HasColumnName("id_time_destino");

                    b.Property<string>("JogadoresAtuais")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("jogadores_atuais")
                        .HasAnnotation("Relational:JsonPropertyName", "jogadores_atuais");

                    b.Property<string>("JogadoresNovos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("jogadores_novos")
                        .HasAnnotation("Relational:JsonPropertyName", "jogadores_novos");

                    b.Property<string>("JogadoresRemovidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("jogadores_removidos")
                        .HasAnnotation("Relational:JsonPropertyName", "jogadores_removidos");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("observacoes");

                    b.Property<int>("TipoNegociacaoJogador")
                        .HasColumnType("int")
                        .HasColumnName("tipo_negociacao_jogador");

                    b.HasKey("Id")
                        .HasName("pk_negociacao_jogador");

                    b.HasIndex("IdTimeDestino")
                        .HasDatabaseName("ix_negociacao_jogador_id_time_destino");

                    b.ToTable("negociacao_jogador", (string)null);
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nome");

                    b.HasKey("Id")
                        .HasName("pk_time");

                    b.ToTable("time", (string)null);
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.NegociacaoJogador", b =>
                {
                    b.HasOne("Negociacoes.WebApi.Models.Entities.Time", "TimeDestino")
                        .WithMany()
                        .HasForeignKey("IdTimeDestino")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_negociacao_jogador_time_time_destino_id");

                    b.Navigation("TimeDestino");
                });
#pragma warning restore 612, 618
        }
    }
}