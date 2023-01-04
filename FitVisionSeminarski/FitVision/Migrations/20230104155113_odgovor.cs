using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class odgovor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odgovor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    poruka_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odgovor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Odgovor_Poruka_poruka_id",
                        column: x => x.poruka_id,
                        principalTable: "Poruka",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Odgovor_poruka_id",
                table: "Odgovor",
                column: "poruka_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odgovor");
        }
    }
}
