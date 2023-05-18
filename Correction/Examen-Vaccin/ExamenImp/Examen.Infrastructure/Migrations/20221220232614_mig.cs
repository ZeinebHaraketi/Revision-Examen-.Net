using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "centreVaccinations",
                columns: table => new
                {
                    CentreVaccinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacite = table.Column<int>(type: "int", nullable: false),
                    NbChaises = table.Column<int>(type: "int", nullable: false),
                    NumTelephone = table.Column<int>(type: "int", nullable: false),
                    ResponsableCentre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_centreVaccinations", x => x.CentreVaccinationId);
                });

            migrationBuilder.CreateTable(
                name: "citoyens",
                columns: table => new
                {
                    CIN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CitoyenId = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Telephone = table.Column<int>(type: "int", nullable: false),
                    NumeroEvax = table.Column<int>(type: "int", nullable: false),
                    Adresse_Rue = table.Column<int>(type: "int", nullable: false),
                    Adresse_Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse_CodePostal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_citoyens", x => x.CIN);
                });

            migrationBuilder.CreateTable(
                name: "vaccins",
                columns: table => new
                {
                    VaccinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    DateValidite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fournisseur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeVaccin = table.Column<int>(type: "int", nullable: false),
                    CentreVaccinationId = table.Column<int>(type: "int", nullable: false),
                    Validité = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaccins", x => x.VaccinId);
                    table.ForeignKey(
                        name: "FK_vaccins_centreVaccinations_CentreVaccinationId",
                        column: x => x.CentreVaccinationId,
                        principalTable: "centreVaccinations",
                        principalColumn: "CentreVaccinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rendezVous",
                columns: table => new
                {
                    DateVaccination = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CitoyenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VaccinId = table.Column<int>(type: "int", nullable: false),
                    NbrDoses = table.Column<int>(type: "int", nullable: false),
                    CodeInfirmiere = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rendezVous", x => new { x.VaccinId, x.CitoyenId, x.DateVaccination });
                    table.ForeignKey(
                        name: "FK_rendezVous_citoyens_CitoyenId",
                        column: x => x.CitoyenId,
                        principalTable: "citoyens",
                        principalColumn: "CIN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rendezVous_vaccins_VaccinId",
                        column: x => x.VaccinId,
                        principalTable: "vaccins",
                        principalColumn: "VaccinId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rendezVous_CitoyenId",
                table: "rendezVous",
                column: "CitoyenId");

            migrationBuilder.CreateIndex(
                name: "IX_vaccins_CentreVaccinationId",
                table: "vaccins",
                column: "CentreVaccinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rendezVous");

            migrationBuilder.DropTable(
                name: "citoyens");

            migrationBuilder.DropTable(
                name: "vaccins");

            migrationBuilder.DropTable(
                name: "centreVaccinations");
        }
    }
}
