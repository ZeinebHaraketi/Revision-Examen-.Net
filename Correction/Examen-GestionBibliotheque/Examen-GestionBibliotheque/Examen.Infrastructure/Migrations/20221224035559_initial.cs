using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Abonnes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategorieCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abonnes_Categories_CategorieCode",
                        column: x => x.CategorieCode,
                        principalTable: "Categories",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livre",
                columns: table => new
                {
                    LivreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbExemplaires = table.Column<int>(type: "int", nullable: false),
                    Auteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategorieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livre", x => x.LivreId);
                    table.ForeignKey(
                        name: "FK_Livre_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PretLivres",
                columns: table => new
                {
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AbonneFK = table.Column<int>(type: "int", nullable: false),
                    LivreFK = table.Column<int>(type: "int", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PretLivres", x => new { x.LivreFK, x.AbonneFK, x.DateDebut });
                    table.ForeignKey(
                        name: "FK_PretLivres_Abonnes_AbonneFK",
                        column: x => x.AbonneFK,
                        principalTable: "Abonnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnes_CategorieCode",
                table: "Abonnes",
                column: "CategorieCode");

            migrationBuilder.CreateIndex(
                name: "IX_Livre_CategorieId",
                table: "Livre",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_PretLivres_AbonneFK",
                table: "PretLivres",
                column: "AbonneFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livre");

            migrationBuilder.DropTable(
                name: "PretLivres");

            migrationBuilder.DropTable(
                name: "Abonnes");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
