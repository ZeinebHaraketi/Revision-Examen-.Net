using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExamenSIM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banques",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banques", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "DABs",
                columns: table => new
                {
                    DABId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Localisation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DABs", x => x.DABId);
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
                name: "Comptes",
                columns: table => new
                {
                    NumeroCompte = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Proprietaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solde = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BanqueFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.NumeroCompte);
                    table.ForeignKey(
                        name: "FK_Comptes_Banques_BanqueFK",
                        column: x => x.BanqueFK,
                        principalTable: "Banques",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompteFk = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DabFK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Montant = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => new { x.DabFK, x.CompteFk, x.Date });
                    table.ForeignKey(
                        name: "FK_Transactions_Comptes_CompteFk",
                        column: x => x.CompteFk,
                        principalTable: "Comptes",
                        principalColumn: "NumeroCompte",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_DABs_DabFK",
                        column: x => x.DabFK,
                        principalTable: "DABs",
                        principalColumn: "DABId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionRetrait",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompteFk = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DabFK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AutreAgence = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRetrait", x => new { x.DabFK, x.CompteFk, x.Date });
                    table.ForeignKey(
                        name: "FK_TransactionRetrait_Comptes_CompteFk",
                        column: x => x.CompteFk,
                        principalTable: "Comptes",
                        principalColumn: "NumeroCompte",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionRetrait_DABs_DabFK",
                        column: x => x.DabFK,
                        principalTable: "DABs",
                        principalColumn: "DABId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionRetrait_Transactions_DabFK_CompteFk_Date",
                        columns: x => new { x.DabFK, x.CompteFk, x.Date },
                        principalTable: "Transactions",
                        principalColumns: new[] { "DabFK", "CompteFk", "Date" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTransfert",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompteFk = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DabFK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumeroCompte = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTransfert", x => new { x.DabFK, x.CompteFk, x.Date });
                    table.ForeignKey(
                        name: "FK_TransactionTransfert_Comptes_CompteFk",
                        column: x => x.CompteFk,
                        principalTable: "Comptes",
                        principalColumn: "NumeroCompte",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionTransfert_DABs_DabFK",
                        column: x => x.DabFK,
                        principalTable: "DABs",
                        principalColumn: "DABId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionTransfert_Transactions_DabFK_CompteFk_Date",
                        columns: x => new { x.DabFK, x.CompteFk, x.Date },
                        principalTable: "Transactions",
                        principalColumns: new[] { "DabFK", "CompteFk", "Date" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comptes_BanqueFK",
                table: "Comptes",
                column: "BanqueFK");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionRetrait_CompteFk",
                table: "TransactionRetrait",
                column: "CompteFk");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CompteFk",
                table: "Transactions",
                column: "CompteFk");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTransfert_CompteFk",
                table: "TransactionTransfert",
                column: "CompteFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exemples");

            migrationBuilder.DropTable(
                name: "TransactionRetrait");

            migrationBuilder.DropTable(
                name: "TransactionTransfert");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "DABs");

            migrationBuilder.DropTable(
                name: "Banques");
        }
    }
}
