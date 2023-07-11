using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Negociacoes.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class torna_idcomposicao_opcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_time_composicao_time_composicao_time_id",
                table: "time");

            migrationBuilder.AlterColumn<int>(
                name: "id_composicao_time",
                table: "time",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_time_composicao_time_composicao_time_id",
                table: "time",
                column: "id_composicao_time",
                principalTable: "composicao_time",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_time_composicao_time_composicao_time_id",
                table: "time");

            migrationBuilder.AlterColumn<int>(
                name: "id_composicao_time",
                table: "time",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_time_composicao_time_composicao_time_id",
                table: "time",
                column: "id_composicao_time",
                principalTable: "composicao_time",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
