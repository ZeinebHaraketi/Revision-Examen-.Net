using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_PretLivres_Livre_LivreFK",
                table: "PretLivres",
                column: "LivreFK",
                principalTable: "Livre",
                principalColumn: "LivreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PretLivres_Livre_LivreFK",
                table: "PretLivres");
        }
    }
}
