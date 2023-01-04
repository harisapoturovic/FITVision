using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class akcija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Akcija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iznos = table.Column<int>(type: "int", nullable: false),
                    DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Akcija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AkcijaProizvod",
                columns: table => new
                {
                    AkcijeID = table.Column<int>(type: "int", nullable: false),
                    ProizvodiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkcijaProizvod", x => new { x.AkcijeID, x.ProizvodiID });
                    table.ForeignKey(
                        name: "FK_AkcijaProizvod_Akcija_AkcijeID",
                        column: x => x.AkcijeID,
                        principalTable: "Akcija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AkcijaProizvod_Proizvod_ProizvodiID",
                        column: x => x.ProizvodiID,
                        principalTable: "Proizvod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AkcijaProizvod_ProizvodiID",
                table: "AkcijaProizvod",
                column: "ProizvodiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AkcijaProizvod");

            migrationBuilder.DropTable(
                name: "Akcija");
        }
    }
}
