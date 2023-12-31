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
    [Migration("20230712010206_allow_null_cc")]
    partial class allow_null_cc
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.ComposicaoCarga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataEntrega")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_entrega");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("observacao");

                    b.Property<int>("Situacao")
                        .HasColumnType("int")
                        .HasColumnName("situacao");

                    b.HasKey("Id")
                        .HasName("pk_composicao_carga");

                    b.HasIndex("IdUsuario")
                        .HasDatabaseName("ix_composicao_carga_id_usuario");

                    b.ToTable("composicao_carga", (string)null);
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.NegociacaoComposicaoCarga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataEvento")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_evento");

                    b.Property<int>("IdComposicaoCarga")
                        .HasColumnType("int")
                        .HasColumnName("id_composicao_carga");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("observacao");

                    b.Property<int>("TipoNegociacao")
                        .HasColumnType("int")
                        .HasColumnName("tipo_negociacao");

                    b.Property<int>("TipoUsuarioResponsavelProximaEtapa")
                        .HasColumnType("int")
                        .HasColumnName("tipo_usuario_responsavel_proxima_etapa");

                    b.HasKey("Id")
                        .HasName("pk_negociacao_composicao_carga");

                    b.HasIndex("IdComposicaoCarga")
                        .HasDatabaseName("ix_negociacao_composicao_carga_id_composicao_carga");

                    b.ToTable("negociacao_composicao_carga", (string)null);
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataEntrega")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_entrega");

                    b.Property<int?>("IdComposicaoCarga")
                        .HasColumnType("int")
                        .HasColumnName("id_composicao_carga");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("item");

                    b.Property<decimal>("Quantidade")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("quantidade");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_pedido");

                    b.HasIndex("IdComposicaoCarga")
                        .HasDatabaseName("ix_pedido_id_composicao_carga");

                    b.ToTable("pedido", (string)null);
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.Sugestao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataEntrega")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_entrega");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("item");

                    b.Property<decimal>("Quantidade")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("quantidade");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_sugestao");

                    b.ToTable("sugestao", (string)null);
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nome");

                    b.Property<int>("TipoUsuario")
                        .HasColumnType("int")
                        .HasColumnName("tipo_usuario");

                    b.HasKey("Id")
                        .HasName("pk_usuario");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.ComposicaoCarga", b =>
                {
                    b.HasOne("Negociacoes.WebApi.Models.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_composicao_carga_usuario_usuario_id");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.NegociacaoComposicaoCarga", b =>
                {
                    b.HasOne("Negociacoes.WebApi.Models.Entities.ComposicaoCarga", "ComposicaoCarga")
                        .WithMany()
                        .HasForeignKey("IdComposicaoCarga")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_negociacao_composicao_carga_composicao_carga_composicao_carga_id");

                    b.OwnsOne("Negociacoes.WebApi.Models.Entities.NegociacaoComposicaoCargaJson", "MetaData", b1 =>
                        {
                            b1.Property<int>("NegociacaoComposicaoCargaId")
                                .HasColumnType("int")
                                .HasColumnName("id");

                            b1.HasKey("NegociacaoComposicaoCargaId");

                            b1.ToTable("negociacao_composicao_carga");

                            b1.ToJson("meta_data");

                            b1.WithOwner()
                                .HasForeignKey("NegociacaoComposicaoCargaId")
                                .HasConstraintName("fk_negociacao_composicao_carga_negociacao_composicao_carga_id");

                            b1.OwnsMany("Negociacoes.WebApi.Models.Entities.IntSugestao", "SugestoesNovas", b2 =>
                                {
                                    b2.Property<int>("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<int>("IdSugestao")
                                        .HasColumnType("int");

                                    b2.HasKey("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId", "Id")
                                        .HasName("pk_negociacao_composicao_carga");

                                    b2.ToTable("negociacao_composicao_carga");

                                    b2.HasAnnotation("Relational:JsonPropertyName", "sugestoes_novas");

                                    b2.WithOwner()
                                        .HasForeignKey("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId")
                                        .HasConstraintName("fk_negociacao_composicao_carga_negociacao_composicao_carga_negociacao_composicao_carga_json_negociacao_composicao_carga_id");
                                });

                            b1.OwnsMany("Negociacoes.WebApi.Models.Entities.IntPedido", "PedidosNovos", b2 =>
                                {
                                    b2.Property<int>("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<int>("IdPedido")
                                        .HasColumnType("int");

                                    b2.HasKey("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId", "Id")
                                        .HasName("pk_negociacao_composicao_carga");

                                    b2.ToTable("negociacao_composicao_carga");

                                    b2.HasAnnotation("Relational:JsonPropertyName", "pedidos_novos");

                                    b2.WithOwner()
                                        .HasForeignKey("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId")
                                        .HasConstraintName("fk_negociacao_composicao_carga_negociacao_composicao_carga_negociacao_composicao_carga_json_negociacao_composicao_carga_id");
                                });

                            b1.OwnsMany("Negociacoes.WebApi.Models.Entities.IntPedido", "PedidosRemovidos", b2 =>
                                {
                                    b2.Property<int>("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<int>("IdPedido")
                                        .HasColumnType("int");

                                    b2.HasKey("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId", "Id")
                                        .HasName("pk_negociacao_composicao_carga");

                                    b2.ToTable("negociacao_composicao_carga");

                                    b2.HasAnnotation("Relational:JsonPropertyName", "pedidos_removidos");

                                    b2.WithOwner()
                                        .HasForeignKey("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId")
                                        .HasConstraintName("fk_negociacao_composicao_carga_negociacao_composicao_carga_negociacao_composicao_carga_json_negociacao_composicao_carga_id");
                                });

                            b1.OwnsMany("Negociacoes.WebApi.Models.Entities.NegociacaoPedidoJson", "PedidosAtuais", b2 =>
                                {
                                    b2.Property<int>("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<int>("IdPedido")
                                        .HasColumnType("int");

                                    b2.Property<decimal>("Quantidade")
                                        .HasColumnType("decimal(18,2)");

                                    b2.HasKey("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId", "Id")
                                        .HasName("pk_negociacao_composicao_carga");

                                    b2.ToTable("negociacao_composicao_carga");

                                    b2.HasAnnotation("Relational:JsonPropertyName", "pedidos_atuais");

                                    b2.WithOwner()
                                        .HasForeignKey("NegociacaoComposicaoCargaJsonNegociacaoComposicaoCargaId")
                                        .HasConstraintName("fk_negociacao_composicao_carga_negociacao_composicao_carga_negociacao_composicao_carga_json_negociacao_composicao_carga_id");
                                });

                            b1.Navigation("PedidosAtuais");

                            b1.Navigation("PedidosNovos");

                            b1.Navigation("PedidosRemovidos");

                            b1.Navigation("SugestoesNovas");
                        });

                    b.Navigation("ComposicaoCarga");

                    b.Navigation("MetaData");
                });

            modelBuilder.Entity("Negociacoes.WebApi.Models.Entities.Pedido", b =>
                {
                    b.HasOne("Negociacoes.WebApi.Models.Entities.ComposicaoCarga", "ComposicaoCarga")
                        .WithMany()
                        .HasForeignKey("IdComposicaoCarga")
                        .HasConstraintName("fk_pedido_composicao_carga_composicao_carga_id");

                    b.Navigation("ComposicaoCarga");
                });
#pragma warning restore 612, 618
        }
    }
}
