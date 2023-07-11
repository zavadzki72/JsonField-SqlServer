using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Negociacoes.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jogador",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jogador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "time",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_time", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "negociacao_jogador",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_evento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tipo_negociacao_jogador = table.Column<int>(type: "int", nullable: false),
                    data_contrato_proposta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_time_destino = table.Column<int>(type: "int", nullable: false),
                    jogadores_atuais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jogadores_novos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jogadores_removidos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_negociacao_jogador", x => x.id);
                    table.ForeignKey(
                        name: "fk_negociacao_jogador_time_time_destino_id",
                        column: x => x.id_time_destino,
                        principalTable: "time",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_negociacao_jogador_id_time_destino",
                table: "negociacao_jogador",
                column: "id_time_destino");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jogador");

            migrationBuilder.DropTable(
                name: "negociacao_jogador");

            migrationBuilder.DropTable(
                name: "time");
        }
    }
}
