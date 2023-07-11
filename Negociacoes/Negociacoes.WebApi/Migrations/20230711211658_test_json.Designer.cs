﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Negociacoes.WebApi.Infra;

#nullable disable

namespace Negociacoes.WebApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230711211658_test_json")]
    partial class test_json
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.ComposicaoTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SituacaoComposicaoTime")
                        .HasColumnType("int")
                        .HasColumnName("situacao_composicao_time");

                    b.HasKey("Id")
                        .HasName("pk_composicao_time");

                    b.ToTable("composicao_time", (string)null);
                });

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

                    b.Property<int>("IdComposicaoTime")
                        .HasColumnType("int")
                        .HasColumnName("id_composicao_time");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("nome");

                    b.HasKey("Id")
                        .HasName("pk_time");

                    b.HasIndex("IdComposicaoTime")
                        .HasDatabaseName("ix_time_id_composicao_time");

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

                    b.OwnsOne("Negociacoes.WebApi.Models.Entities.NegociacaoJogador.JogadoresNovos#List", "JogadoresNovos", b1 =>
                        {
                            b1.Property<int>("NegociacaoJogadorId")
                                .HasColumnType("int");

                            b1.Property<int>("Capacity")
                                .HasColumnType("int");

                            b1.HasKey("NegociacaoJogadorId")
                                .HasName("pk_negociacao_jogador");

                            b1.ToTable("negociacao_jogador");

                            b1.ToJson("jogadores_novos");

                            b1.WithOwner()
                                .HasForeignKey("NegociacaoJogadorId")
                                .HasConstraintName("fk_negociacao_jogador_negociacao_jogador_negociacao_jogador_id");
                        });

                    b.OwnsOne("System.Collections.Generic.List<int>", "JogadoresRemovidos", b1 =>
                        {
                            b1.Property<int>("NegociacaoJogadorId")
                                .HasColumnType("int")
                                .HasColumnName("id");

                            b1.Property<int>("Capacity")
                                .HasColumnType("int")
                                .HasColumnName("jogadores_removidos_capacity");

                            b1.HasKey("NegociacaoJogadorId");

                            b1.ToTable("negociacao_jogador");

                            b1.ToJson("jogadores_removidos");

                            b1.WithOwner()
                                .HasForeignKey("NegociacaoJogadorId")
                                .HasConstraintName("fk_negociacao_jogador_negociacao_jogador_id");
                        });

                    b.OwnsOne("System.Collections.Generic.List<Negociacoes.WebApi.Models.Entities.Jogador>", "JogadoresAtuais", b1 =>
                        {
                            b1.Property<int>("NegociacaoJogadorId")
                                .HasColumnType("int")
                                .HasColumnName("id");

                            b1.Property<int>("Capacity")
                                .HasColumnType("int")
                                .HasColumnName("jogadores_atuais_capacity");

                            b1.HasKey("NegociacaoJogadorId");

                            b1.ToTable("negociacao_jogador");

                            b1.ToJson("jogadores_atuais");

                            b1.WithOwner()
                                .HasForeignKey("NegociacaoJogadorId")
                                .HasConstraintName("fk_negociacao_jogador_negociacao_jogador_id");
                        });

                    b.Navigation("JogadoresAtuais")
                        .IsRequired();

                    b.Navigation("JogadoresNovos")
                        .IsRequired();

                    b.Navigation("JogadoresRemovidos")
                        .IsRequired();

                    b.Navigation("TimeDestino");
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.Time", b =>
                {
                    b.HasOne("Negociacoes.WebApi.Models.Entities.ComposicaoTime", "ComposicaoTime")
                        .WithMany()
                        .HasForeignKey("IdComposicaoTime")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_time_composicao_time_composicao_time_id");

                    b.Navigation("ComposicaoTime");
                });
#pragma warning restore 612, 618
        }
    }
}
