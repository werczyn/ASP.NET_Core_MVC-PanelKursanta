using Microsoft.EntityFrameworkCore.Migrations;

namespace IPBProjekt.Migrations
{
    public partial class Uzytkownik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    Login = table.Column<string>(nullable: false),
                    Haslo = table.Column<string>(nullable: true),
                    Grupa = table.Column<string>(nullable: true),
                    IdOsoba = table.Column<int>(nullable: true),
                    WydzialKomunikacjiNumerWydzialu = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.Login);
                    table.ForeignKey(
                        name: "FK_Uzytkownicy_Kursants_IdOsoba",
                        column: x => x.IdOsoba,
                        principalTable: "Kursants",
                        principalColumn: "IdOsoba",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uzytkownicy_WydzialKomunikacji_WydzialKomunikacjiNumerWydzialu",
                        column: x => x.WydzialKomunikacjiNumerWydzialu,
                        principalTable: "WydzialKomunikacji",
                        principalColumn: "NumerWydzialu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_IdOsoba",
                table: "Uzytkownicy",
                column: "IdOsoba",
                unique: true,
                filter: "[IdOsoba] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_WydzialKomunikacjiNumerWydzialu",
                table: "Uzytkownicy",
                column: "WydzialKomunikacjiNumerWydzialu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uzytkownicy");
        }
    }
}
