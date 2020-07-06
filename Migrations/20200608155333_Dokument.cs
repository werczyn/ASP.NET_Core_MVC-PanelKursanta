using Microsoft.EntityFrameworkCore.Migrations;

namespace IPBProjekt.Migrations
{
    public partial class Dokument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdDokument",
                table: "Kursants",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dokumenty",
                columns: table => new
                {
                    IdDokument = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOsoba = table.Column<int>(nullable: true),
                    IdWydzialKomunikacji = table.Column<int>(nullable: true),
                    CzySprawdzony = table.Column<bool>(nullable: false),
                    CzyPrzyjety = table.Column<bool>(nullable: false),
                    CzyWyslany = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumenty", x => x.IdDokument);
                    table.ForeignKey(
                        name: "FK_Dokumenty_Kursants_IdOsoba",
                        column: x => x.IdOsoba,
                        principalTable: "Kursants",
                        principalColumn: "IdOsoba",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dokumenty_WydzialKomunikacji_IdWydzialKomunikacji",
                        column: x => x.IdWydzialKomunikacji,
                        principalTable: "WydzialKomunikacji",
                        principalColumn: "NumerWydzialu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kursants_IdDokument",
                table: "Kursants",
                column: "IdDokument");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenty_IdOsoba",
                table: "Dokumenty",
                column: "IdOsoba");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenty_IdWydzialKomunikacji",
                table: "Dokumenty",
                column: "IdWydzialKomunikacji");

            migrationBuilder.AddForeignKey(
                name: "FK_Kursants_Dokumenty_IdDokument",
                table: "Kursants",
                column: "IdDokument",
                principalTable: "Dokumenty",
                principalColumn: "IdDokument",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kursants_Dokumenty_IdDokument",
                table: "Kursants");

            migrationBuilder.DropTable(
                name: "Dokumenty");

            migrationBuilder.DropIndex(
                name: "IX_Kursants_IdDokument",
                table: "Kursants");

            migrationBuilder.DropColumn(
                name: "IdDokument",
                table: "Kursants");
        }
    }
}
