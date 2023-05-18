using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abonnes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Code);
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
                name: "Livres",
                columns: table => new
                {
                    LivreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbrExemplaires = table.Column<int>(type: "int", nullable: false),
                    Auteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategorieCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livres", x => x.LivreId);
                    table.ForeignKey(
                        name: "FK_Livres_Categories_CategorieCode",
                        column: x => x.CategorieCode,
                        principalTable: "Categories",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PretLivres",
                columns: table => new
                {
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LivreFk = table.Column<int>(type: "int", nullable: false),
                    AbonneFk = table.Column<int>(type: "int", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LivreId = table.Column<int>(type: "int", nullable: false),
                    AbonneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PretLivres", x => new { x.LivreFk, x.AbonneFk, x.DateDebut });
                    table.ForeignKey(
                        name: "FK_PretLivres_Abonnes_AbonneId",
                        column: x => x.AbonneId,
                        principalTable: "Abonnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PretLivres_Livres_LivreId",
                        column: x => x.LivreId,
                        principalTable: "Livres",
                        principalColumn: "LivreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livres_CategorieCode",
                table: "Livres",
                column: "CategorieCode");

            migrationBuilder.CreateIndex(
                name: "IX_PretLivres_AbonneId",
                table: "PretLivres",
                column: "AbonneId");

            migrationBuilder.CreateIndex(
                name: "IX_PretLivres_LivreId",
                table: "PretLivres",
                column: "LivreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exemples");

            migrationBuilder.DropTable(
                name: "PretLivres");

            migrationBuilder.DropTable(
                name: "Abonnes");

            migrationBuilder.DropTable(
                name: "Livres");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
