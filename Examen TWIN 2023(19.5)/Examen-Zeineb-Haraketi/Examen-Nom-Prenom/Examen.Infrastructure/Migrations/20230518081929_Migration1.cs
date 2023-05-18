using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresse = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
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
                name: "Prestataires",
                columns: table => new
                {
                    PrestataireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<int>(type: "int", nullable: false),
                    PageInstagram = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PrestataireNom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PrestataireTel = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Zone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestataires", x => x.PrestataireId);
                });

            migrationBuilder.CreateTable(
                name: "Prestations",
                columns: table => new
                {
                    PrestationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PrestationType = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    PrestataireFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestations", x => x.PrestationId);
                    table.ForeignKey(
                        name: "FK_Prestations_Prestataires_PrestataireFk",
                        column: x => x.PrestataireFk,
                        principalTable: "Prestataires",
                        principalColumn: "PrestataireId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RDVs",
                columns: table => new
                {
                    DateRDV = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrestationFK = table.Column<int>(type: "int", nullable: false),
                    ClientFK = table.Column<int>(type: "int", nullable: false),
                    Confirmation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RDVs", x => new { x.PrestationFK, x.ClientFK, x.DateRDV });
                    table.ForeignKey(
                        name: "FK_RDVs_Clients_ClientFK",
                        column: x => x.ClientFK,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RDVs_Prestations_PrestationFK",
                        column: x => x.PrestationFK,
                        principalTable: "Prestations",
                        principalColumn: "PrestationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prestations_PrestataireFk",
                table: "Prestations",
                column: "PrestataireFk");

            migrationBuilder.CreateIndex(
                name: "IX_RDVs_ClientFK",
                table: "RDVs",
                column: "ClientFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exemples");

            migrationBuilder.DropTable(
                name: "RDVs");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Prestations");

            migrationBuilder.DropTable(
                name: "Prestataires");
        }
    }
}
