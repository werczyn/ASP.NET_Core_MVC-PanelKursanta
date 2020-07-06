using Microsoft.EntityFrameworkCore.Migrations;

namespace IPBProjekt.Migrations
{
    public partial class Uzytkownik_id_poprawka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzytkownicy_WydzialKomunikacji_WydzialKomunikacjiNumerWydzialu",
                table: "Uzytkownicy");

            migrationBuilder.DropIndex(
                name: "IX_Uzytkownicy_WydzialKomunikacjiNumerWydzialu",
                table: "Uzytkownicy");

            migrationBuilder.DropColumn(
                name: "WydzialKomunikacjiNumerWydzialu",
                table: "Uzytkownicy");

            migrationBuilder.AddColumn<int>(
                name: "NumerWydzialu",
                table: "Uzytkownicy",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_NumerWydzialu",
                table: "Uzytkownicy",
                column: "NumerWydzialu");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzytkownicy_WydzialKomunikacji_NumerWydzialu",
                table: "Uzytkownicy",
                column: "NumerWydzialu",
                principalTable: "WydzialKomunikacji",
                principalColumn: "NumerWydzialu",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzytkownicy_WydzialKomunikacji_NumerWydzialu",
                table: "Uzytkownicy");

            migrationBuilder.DropIndex(
                name: "IX_Uzytkownicy_NumerWydzialu",
                table: "Uzytkownicy");

            migrationBuilder.DropColumn(
                name: "NumerWydzialu",
                table: "Uzytkownicy");

            migrationBuilder.AddColumn<int>(
                name: "WydzialKomunikacjiNumerWydzialu",
                table: "Uzytkownicy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_WydzialKomunikacjiNumerWydzialu",
                table: "Uzytkownicy",
                column: "WydzialKomunikacjiNumerWydzialu");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzytkownicy_WydzialKomunikacji_WydzialKomunikacjiNumerWydzialu",
                table: "Uzytkownicy",
                column: "WydzialKomunikacjiNumerWydzialu",
                principalTable: "WydzialKomunikacji",
                principalColumn: "NumerWydzialu",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
