using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class migg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CijenaSPopustom",
                table: "KorpaProizvod");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CijenaSPopustom",
                table: "KorpaProizvod",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
