using Microsoft.EntityFrameworkCore.Migrations;

namespace IPBProjekt.Migrations
{
    public partial class OsrodekSzkoleniaKierowcow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OsrodekSzkoleniaKierowcowIdOsrodka",
                table: "Kursants",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OsrodekSzkoleniaKierowcow",
                columns: table => new
                {
                    IdOsrodka = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miasto = table.Column<string>(nullable: true),
                    Ulica = table.Column<string>(nullable: true),
                    NumerMieszkania = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsrodekSzkoleniaKierowcow", x => x.IdOsrodka);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kursants_OsrodekSzkoleniaKierowcowIdOsrodka",
                table: "Kursants",
                column: "OsrodekSzkoleniaKierowcowIdOsrodka");

            migrationBuilder.AddForeignKey(
                name: "FK_Kursants_OsrodekSzkoleniaKierowcow_OsrodekSzkoleniaKierowcowIdOsrodka",
                table: "Kursants",
                column: "OsrodekSzkoleniaKierowcowIdOsrodka",
                principalTable: "OsrodekSzkoleniaKierowcow",
                principalColumn: "IdOsrodka",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kursants_OsrodekSzkoleniaKierowcow_OsrodekSzkoleniaKierowcowIdOsrodka",
                table: "Kursants");

            migrationBuilder.DropTable(
                name: "OsrodekSzkoleniaKierowcow");

            migrationBuilder.DropIndex(
                name: "IX_Kursants_OsrodekSzkoleniaKierowcowIdOsrodka",
                table: "Kursants");

            migrationBuilder.DropColumn(
                name: "OsrodekSzkoleniaKierowcowIdOsrodka",
                table: "Kursants");
        }
    }
}
