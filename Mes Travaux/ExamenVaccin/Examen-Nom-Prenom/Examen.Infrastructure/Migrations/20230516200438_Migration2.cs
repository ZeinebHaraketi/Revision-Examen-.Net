using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentreVaccinations",
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
                    table.PrimaryKey("PK_CentreVaccinations", x => x.CentreVaccinationId);
                });

            migrationBuilder.CreateTable(
                name: "Citoyens",
                columns: table => new
                {
                    CIN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdresseCodePostal = table.Column<int>(name: "Adresse_CodePostal", type: "int", nullable: false),
                    AdresseRue = table.Column<int>(name: "Adresse_Rue", type: "int", nullable: false),
                    AdresseVille = table.Column<string>(name: "Adresse_Ville", type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CitoyenId = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroEvax = table.Column<int>(type: "int", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citoyens", x => x.CIN);
                });

            migrationBuilder.CreateTable(
                name: "Exemples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exemples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaccins",
                columns: table => new
                {
                    VaccinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateValidite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fournisseur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    TypeVaccin = table.Column<int>(type: "int", nullable: false),
                    CentreVaccinationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccins", x => x.VaccinId);
                    table.ForeignKey(
                        name: "FK_Vaccins_CentreVaccinations_CentreVaccinationId",
                        column: x => x.CentreVaccinationId,
                        principalTable: "CentreVaccinations",
                        principalColumn: "CentreVaccinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RendezVous",
                columns: table => new
                {
                    DateVaccination = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CitoyenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VaccinId = table.Column<int>(type: "int", nullable: false),
                    CodeInfermiere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbrDoses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendezVous", x => new { x.CitoyenId, x.VaccinId, x.DateVaccination });
                    table.ForeignKey(
                        name: "FK_RendezVous_Citoyens_CitoyenId",
                        column: x => x.CitoyenId,
                        principalTable: "Citoyens",
                        principalColumn: "CIN",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RendezVous_Vaccins_VaccinId",
                        column: x => x.VaccinId,
                        principalTable: "Vaccins",
                        principalColumn: "VaccinId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_VaccinId",
                table: "RendezVous",
                column: "VaccinId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccins_CentreVaccinationId",
                table: "Vaccins",
                column: "CentreVaccinationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exemples");

            migrationBuilder.DropTable(
                name: "RendezVous");

            migrationBuilder.DropTable(
                name: "Citoyens");

            migrationBuilder.DropTable(
                name: "Vaccins");

            migrationBuilder.DropTable(
                name: "CentreVaccinations");
        }
    }
}
