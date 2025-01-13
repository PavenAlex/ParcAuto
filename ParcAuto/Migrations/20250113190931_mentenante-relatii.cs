using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcAuto.Migrations
{
    /// <inheritdoc />
    public partial class mentenanterelatii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Mentenantas_ID_Vehicul",
                table: "Mentenantas",
                column: "ID_Vehicul");

            migrationBuilder.AddForeignKey(
                name: "FK_Mentenantas_Vehicles_ID_Vehicul",
                table: "Mentenantas",
                column: "ID_Vehicul",
                principalTable: "Vehicles",
                principalColumn: "ID_Vehicul",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mentenantas_Vehicles_ID_Vehicul",
                table: "Mentenantas");

            migrationBuilder.DropIndex(
                name: "IX_Mentenantas_ID_Vehicul",
                table: "Mentenantas");
        }
    }
}
