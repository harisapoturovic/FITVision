using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class forum_odgovor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumOdgovor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Odgovor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    forumTema_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumOdgovor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ForumOdgovor_ForumTema_forumTema_id",
                        column: x => x.forumTema_id,
                        principalTable: "ForumTema",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumOdgovor_forumTema_id",
                table: "ForumOdgovor",
                column: "forumTema_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumOdgovor");
        }
    }
}
