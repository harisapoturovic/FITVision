using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVision.Migrations
{
    public partial class proizvod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JedinicnaCijena = table.Column<float>(type: "real", nullable: false),
                    Sastav = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JedinicnaMjera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zaliha = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pod_kategorijaid = table.Column<int>(type: "int", nullable: false),
                    brendid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Proizvod_Brend_brendid",
                        column: x => x.brendid,
                        principalTable: "Brend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvod_Podkategorija_pod_kategorijaid",
                        column: x => x.pod_kategorijaid,
                        principalTable: "Podkategorija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_brendid",
                table: "Proizvod",
                column: "brendid");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_pod_kategorijaid",
                table: "Proizvod",
                column: "pod_kategorijaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proizvod");
        }
    }
}
