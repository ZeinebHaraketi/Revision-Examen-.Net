using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    public partial class typel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Paiement_PaiementEnLigne",
                table: "Courses",
                newName: "PaiementEnLigne");

            migrationBuilder.RenameColumn(
                name: "Paiement_Montant",
                table: "Courses",
                newName: "Montant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaiementEnLigne",
                table: "Courses",
                newName: "Paiement_PaiementEnLigne");

            migrationBuilder.RenameColumn(
                name: "Montant",
                table: "Courses",
                newName: "Paiement_Montant");
        }
    }
}
