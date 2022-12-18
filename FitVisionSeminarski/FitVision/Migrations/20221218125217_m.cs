using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class m : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojStanovnika",
                table: "Drzava");

            migrationBuilder.DropColumn(
                name: "Kontinent",
                table: "Drzava");

            migrationBuilder.AddColumn<string>(
                name: "Skracenica",
                table: "Drzava",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Skracenica",
                table: "Drzava");

            migrationBuilder.AddColumn<int>(
                name: "BrojStanovnika",
                table: "Drzava",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Kontinent",
                table: "Drzava",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
