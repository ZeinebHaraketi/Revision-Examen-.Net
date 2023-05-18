using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    public partial class f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    CIN = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.CIN);
                });

            migrationBuilder.CreateTable(
                name: "Voitures",
                columns: table => new
                {
                    NumMat = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Couleur = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Marque = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voitures", x => x.NumMat);
                });

            migrationBuilder.CreateTable(
                name: "Chauffeurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TauxBenefice = table.Column<float>(type: "real", nullable: false),
                    VoitureNumMat = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chauffeurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chauffeurs_Voitures_VoitureNumMat",
                        column: x => x.VoitureNumMat,
                        principalTable: "Voitures",
                        principalColumn: "NumMat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    DateCourse = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoitureId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LieuDepart = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LieuArrivee = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Paiement_Montant = table.Column<double>(type: "float", nullable: false),
                    Paiement_PaiementEnLigne = table.Column<bool>(type: "bit", nullable: false),
                    Etat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => new { x.DateCourse, x.VoitureId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_Courses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "CIN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Voitures_VoitureId",
                        column: x => x.VoitureId,
                        principalTable: "Voitures",
                        principalColumn: "NumMat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chauffeurs_VoitureNumMat",
                table: "Chauffeurs",
                column: "VoitureNumMat");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ClientId",
                table: "Courses",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_VoitureId",
                table: "Courses",
                column: "VoitureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chauffeurs");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Voitures");
        }
    }
}
