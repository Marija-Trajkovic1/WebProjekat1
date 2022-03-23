using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Back.Migrations
{
    public partial class Version1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aerodrom",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivAerodroma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aerodrom", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Destinacija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivDestinacije = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinacija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Putnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojPasosa = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TezinaPrtljagaUKg = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Putnik", x => x.ID);
                });

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

            migrationBuilder.CreateTable(
                name: "Let",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VremePoletanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremeSletanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UkupanBrojSedista = table.Column<int>(type: "int", nullable: false),
                    BrojZauzetih = table.Column<int>(type: "int", nullable: false),
                    LetoviDestinacijeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Let", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Let_Destinacija_LetoviDestinacijeID",
                        column: x => x.LetoviDestinacijeID,
                        principalTable: "Destinacija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sediste",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RedniBrojSedista = table.Column<int>(type: "int", nullable: false),
                    RezervisanoSediste = table.Column<bool>(type: "bit", nullable: false),
                    TipSedista = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SedistePutnikID = table.Column<int>(type: "int", nullable: true),
                    SedisteLetID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sediste", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sediste_Let_SedisteLetID",
                        column: x => x.SedisteLetID,
                        principalTable: "Let",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sediste_Putnik_SedistePutnikID",
                        column: x => x.SedistePutnikID,
                        principalTable: "Putnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AerodromDestinacija_DestinacijaAerodromID",
                table: "AerodromDestinacija",
                column: "DestinacijaAerodromID");

            migrationBuilder.CreateIndex(
                name: "IX_Let_LetoviDestinacijeID",
                table: "Let",
                column: "LetoviDestinacijeID");

            migrationBuilder.CreateIndex(
                name: "IX_Sediste_SedisteLetID",
                table: "Sediste",
                column: "SedisteLetID");

            migrationBuilder.CreateIndex(
                name: "IX_Sediste_SedistePutnikID",
                table: "Sediste",
                column: "SedistePutnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AerodromDestinacija");

            migrationBuilder.DropTable(
                name: "Sediste");

            migrationBuilder.DropTable(
                name: "Aerodrom");

            migrationBuilder.DropTable(
                name: "Let");

            migrationBuilder.DropTable(
                name: "Putnik");

            migrationBuilder.DropTable(
                name: "Destinacija");
        }
    }
}
