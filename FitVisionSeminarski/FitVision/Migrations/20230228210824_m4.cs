using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class m4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "twoFCode",
                table: "AutentifikacijaToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "twoFJelOtkljucano",
                table: "AutentifikacijaToken",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "twoFCode",
                table: "AutentifikacijaToken");

            migrationBuilder.DropColumn(
                name: "twoFJelOtkljucano",
                table: "AutentifikacijaToken");
        }
    }
}
