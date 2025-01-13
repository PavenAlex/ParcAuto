using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcAuto.Migrations
{
    /// <inheritdoc />
    public partial class relatiirezervari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rezervares_ID_Client",
                table: "Rezervares",
                column: "ID_Client");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervares_ID_Vehicul",
                table: "Rezervares",
                column: "ID_Vehicul");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervares_Clients_ID_Client",
                table: "Rezervares",
                column: "ID_Client",
                principalTable: "Clients",
                principalColumn: "ID_Client",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervares_Vehicles_ID_Vehicul",
                table: "Rezervares",
                column: "ID_Vehicul",
                principalTable: "Vehicles",
                principalColumn: "ID_Vehicul",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervares_Clients_ID_Client",
                table: "Rezervares");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervares_Vehicles_ID_Vehicul",
                table: "Rezervares");

            migrationBuilder.DropIndex(
                name: "IX_Rezervares_ID_Client",
                table: "Rezervares");

            migrationBuilder.DropIndex(
                name: "IX_Rezervares_ID_Vehicul",
                table: "Rezervares");
        }
    }
}
