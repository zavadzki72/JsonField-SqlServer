using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Negociacoes.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class test_json_dois : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jogadores_atuais",
                table: "negociacao_jogador");

            migrationBuilder.DropColumn(
                name: "jogadores_novos",
                table: "negociacao_jogador");

            migrationBuilder.RenameColumn(
                name: "jogadores_removidos",
                table: "negociacao_jogador",
                newName: "negociacao_jogador_json");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "negociacao_jogador_json",
                table: "negociacao_jogador",
                newName: "jogadores_removidos");

            migrationBuilder.AddColumn<string>(
                name: "jogadores_atuais",
                table: "negociacao_jogador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "jogadores_novos",
                table: "negociacao_jogador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
