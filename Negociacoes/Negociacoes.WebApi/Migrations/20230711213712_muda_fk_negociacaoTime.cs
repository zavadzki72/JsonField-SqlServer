using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Negociacoes.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class muda_fk_negociacaoTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_negociacao_jogador_time_time_destino_id",
                table: "negociacao_jogador");

            migrationBuilder.RenameColumn(
                name: "id_time_destino",
                table: "negociacao_jogador",
                newName: "id_composicao_time");

            migrationBuilder.RenameIndex(
                name: "ix_negociacao_jogador_id_time_destino",
                table: "negociacao_jogador",
                newName: "ix_negociacao_jogador_id_composicao_time");

            migrationBuilder.AddForeignKey(
                name: "fk_negociacao_jogador_composicao_time_composicao_time_id",
                table: "negociacao_jogador",
                column: "id_composicao_time",
                principalTable: "composicao_time",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_negociacao_jogador_composicao_time_composicao_time_id",
                table: "negociacao_jogador");

            migrationBuilder.RenameColumn(
                name: "id_composicao_time",
                table: "negociacao_jogador",
                newName: "id_time_destino");

            migrationBuilder.RenameIndex(
                name: "ix_negociacao_jogador_id_composicao_time",
                table: "negociacao_jogador",
                newName: "ix_negociacao_jogador_id_time_destino");

            migrationBuilder.AddForeignKey(
                name: "fk_negociacao_jogador_time_time_destino_id",
                table: "negociacao_jogador",
                column: "id_time_destino",
                principalTable: "time",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
