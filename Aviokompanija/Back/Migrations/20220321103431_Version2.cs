using Microsoft.EntityFrameworkCore.Migrations;

namespace Back.Migrations
{
    public partial class Version2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AerodromDestinacija");

            migrationBuilder.AddColumn<int>(
                name: "DestinacijaAerodromID",
                table: "Destinacija",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Destinacija_DestinacijaAerodromID",
                table: "Destinacija",
                column: "DestinacijaAerodromID");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinacija_Aerodrom_DestinacijaAerodromID",
                table: "Destinacija",
                column: "DestinacijaAerodromID",
                principalTable: "Aerodrom",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinacija_Aerodrom_DestinacijaAerodromID",
                table: "Destinacija");

            migrationBuilder.DropIndex(
                name: "IX_Destinacija_DestinacijaAerodromID",
                table: "Destinacija");

            migrationBuilder.DropColumn(
                name: "DestinacijaAerodromID",
                table: "Destinacija");

            migrationBuilder.CreateTable(
                name: "AerodromDestinacija",
                columns: table => new
                {
                    AerodromDestinacijeID = table.Column<int>(type: "int", nullable: false),
                    DestinacijaAerodromID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AerodromDestinacija", x => new { x.AerodromDestinacijeID, x.DestinacijaAerodromID });
                    table.ForeignKey(
                        name: "FK_AerodromDestinacija_Aerodrom_DestinacijaAerodromID",
                        column: x => x.DestinacijaAerodromID,
                        principalTable: "Aerodrom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AerodromDestinacija_Destinacija_AerodromDestinacijeID",
                        column: x => x.AerodromDestinacijeID,
                        principalTable: "Destinacija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AerodromDestinacija_DestinacijaAerodromID",
                table: "AerodromDestinacija",
                column: "DestinacijaAerodromID");
        }
    }
}
