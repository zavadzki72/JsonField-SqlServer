using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Negociacoes.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class test_json : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_composicao_time",
                table: "time",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "composicao_time",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    situacao_composicao_time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_composicao_time", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_time_id_composicao_time",
                table: "time",
                column: "id_composicao_time");

            migrationBuilder.AddForeignKey(
                name: "fk_time_composicao_time_composicao_time_id",
                table: "time",
                column: "id_composicao_time",
                principalTable: "composicao_time",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_time_composicao_time_composicao_time_id",
                table: "time");

            migrationBuilder.DropTable(
                name: "composicao_time");

            migrationBuilder.DropIndex(
                name: "ix_time_id_composicao_time",
                table: "time");

            migrationBuilder.DropColumn(
                name: "id_composicao_time",
                table: "time");
        }
    }
}
