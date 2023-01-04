using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class forum_tema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumTema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pitanje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    korisnickiNalogID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumTema", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ForumTema_KorisnickiNalog_korisnickiNalogID",
                        column: x => x.korisnickiNalogID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumTema_korisnickiNalogID",
                table: "ForumTema",
                column: "korisnickiNalogID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumTema");
        }
    }
}
