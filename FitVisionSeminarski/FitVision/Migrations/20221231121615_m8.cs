using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class m8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korpa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KorpaProizvod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    Popust = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    proizvodID = table.Column<int>(type: "int", nullable: false),
                    korpaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorpaProizvod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorpaProizvod_Korpa_korpaID",
                        column: x => x.korpaID,
                        principalTable: "Korpa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorpaProizvod_Proizvod_proizvodID",
                        column: x => x.proizvodID,
                        principalTable: "Proizvod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorpaProizvod_korpaID",
                table: "KorpaProizvod",
                column: "korpaID");

            migrationBuilder.CreateIndex(
                name: "IX_KorpaProizvod_proizvodID",
                table: "KorpaProizvod",
                column: "proizvodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorpaProizvod");

            migrationBuilder.DropTable(
                name: "Korpa");
        }
    }
}
