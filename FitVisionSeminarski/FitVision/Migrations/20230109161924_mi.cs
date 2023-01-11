using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class mi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "cijenaPopust",
                table: "KorpaProizvod",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cijenaPopust",
                table: "KorpaProizvod");
        }
    }
}
