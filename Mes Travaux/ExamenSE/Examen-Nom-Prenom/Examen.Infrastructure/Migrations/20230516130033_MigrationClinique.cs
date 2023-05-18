using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationClinique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliniques",
                columns: table => new
                {
                    CliniqueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacite = table.Column<int>(type: "int", nullable: false),
                    NumTel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliniques", x => x.CliniqueId);
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
                name: "Patients",
                columns: table => new
                {
                    NumDossier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresseEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomCompletNom = table.Column<string>(name: "NomComplet_Nom", type: "nvarchar(max)", nullable: false),
                    NomCompletPrenom = table.Column<string>(name: "NomComplet_Prenom", type: "nvarchar(max)", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumTel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.NumDossier);
                });

            migrationBuilder.CreateTable(
                name: "Chambres",
                columns: table => new
                {
                    NumeroChambre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prix = table.Column<float>(type: "real", nullable: false),
                    TypeChambre = table.Column<int>(type: "int", nullable: false),
                    CliniqueFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chambres", x => x.NumeroChambre);
                    table.ForeignKey(
                        name: "FK_Chambres_Cliniques_CliniqueFK",
                        column: x => x.CliniqueFK,
                        principalTable: "Cliniques",
                        principalColumn: "CliniqueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    DateAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChambreFK = table.Column<int>(type: "int", nullable: false),
                    PatientFk = table.Column<int>(type: "int", nullable: false),
                    MotifAdmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbJours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => new { x.PatientFk, x.ChambreFK, x.DateAdmission });
                    table.ForeignKey(
                        name: "FK_Admissions_Chambres_ChambreFK",
                        column: x => x.ChambreFK,
                        principalTable: "Chambres",
                        principalColumn: "NumeroChambre",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admissions_Patients_PatientFk",
                        column: x => x.PatientFk,
                        principalTable: "Patients",
                        principalColumn: "NumDossier",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_ChambreFK",
                table: "Admissions",
                column: "ChambreFK");

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_CliniqueFK",
                table: "Chambres",
                column: "CliniqueFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admissions");

            migrationBuilder.DropTable(
                name: "Exemples");

            migrationBuilder.DropTable(
                name: "Chambres");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Cliniques");
        }
    }
}
