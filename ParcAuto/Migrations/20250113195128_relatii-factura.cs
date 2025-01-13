using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcAuto.Migrations
{
    /// <inheritdoc />
    public partial class relatiifactura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ID_Rezervare",
                table: "Facturas",
                column: "ID_Rezervare",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Rezervares_ID_Rezervare",
                table: "Facturas",
                column: "ID_Rezervare",
                principalTable: "Rezervares",
                principalColumn: "ID_Rezervare",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Rezervares_ID_Rezervare",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_ID_Rezervare",
                table: "Facturas");
        }
    }
}
