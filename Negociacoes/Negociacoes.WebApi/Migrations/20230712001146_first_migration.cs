using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Negociacoes.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class first_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sugestao",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_entrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    quantidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sugestao", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipo_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "composicao_carga",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    data_entrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<int>(type: "int", nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_composicao_carga", x => x.id);
                    table.ForeignKey(
                        name: "fk_composicao_carga_usuario_usuario_id",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "negociacao_composicao_carga",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_evento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_composicao_carga = table.Column<int>(type: "int", nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipo_negociacao = table.Column<int>(type: "int", nullable: false),
                    tipo_usuario_responsavel_proxima_etapa = table.Column<int>(type: "int", nullable: false),
                    meta_data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_negociacao_composicao_carga", x => x.id);
                    table.ForeignKey(
                        name: "fk_negociacao_composicao_carga_composicao_carga_composicao_carga_id",
                        column: x => x.id_composicao_carga,
                        principalTable: "composicao_carga",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_composicao_carga = table.Column<int>(type: "int", nullable: true),
                    data_entrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    quantidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedido", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedido_composicao_carga_composicao_carga_id",
                        column: x => x.id_composicao_carga,
                        principalTable: "composicao_carga",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_composicao_carga_id_usuario",
                table: "composicao_carga",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "ix_negociacao_composicao_carga_id_composicao_carga",
                table: "negociacao_composicao_carga",
                column: "id_composicao_carga");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_id_composicao_carga",
                table: "pedido",
                column: "id_composicao_carga");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "negociacao_composicao_carga");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "sugestao");

            migrationBuilder.DropTable(
                name: "composicao_carga");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
