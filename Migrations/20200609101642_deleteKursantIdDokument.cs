using Microsoft.EntityFrameworkCore.Migrations;

namespace IPBProjekt.Migrations
{
    public partial class deleteKursantIdDokument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kursants_Dokumenty_IdDokument",
                table: "Kursants");

            migrationBuilder.DropIndex(
                name: "IX_Kursants_IdDokument",
                table: "Kursants");

            migrationBuilder.DropIndex(
                name: "IX_Dokumenty_IdOsoba",
                table: "Dokumenty");

            migrationBuilder.DropColumn(
                name: "IdDokument",
                table: "Kursants");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenty_IdOsoba",
                table: "Dokumenty",
                column: "IdOsoba",
                unique: true,
                filter: "[IdOsoba] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dokumenty_IdOsoba",
                table: "Dokumenty");

            migrationBuilder.AddColumn<int>(
                name: "IdDokument",
                table: "Kursants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kursants_IdDokument",
                table: "Kursants",
                column: "IdDokument");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenty_IdOsoba",
                table: "Dokumenty",
                column: "IdOsoba");

            migrationBuilder.AddForeignKey(
                name: "FK_Kursants_Dokumenty_IdDokument",
                table: "Kursants",
                column: "IdDokument",
                principalTable: "Dokumenty",
                principalColumn: "IdDokument",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
