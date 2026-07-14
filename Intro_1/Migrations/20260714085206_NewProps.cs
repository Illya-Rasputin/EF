using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intro_1.Migrations
{
    /// <inheritdoc />
    public partial class NewProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Copies",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PlayMode",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Single Player");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copies",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayMode",
                table: "Games");
        }
    }
}
