using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcAuto.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID_Client = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Inregistrarii = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID_Client);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    ID_Factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Rezervare = table.Column<int>(type: "int", nullable: false),
                    Data_Emitere = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Suma_Totala = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status_Plata = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.ID_Factura);
                });

            migrationBuilder.CreateTable(
                name: "Mentenantas",
                columns: table => new
                {
                    ID_Mentenanta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Vehicul = table.Column<int>(type: "int", nullable: false),
                    Tip_Interventie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Programare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost_Estimativ = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentenantas", x => x.ID_Mentenanta);
                });

            migrationBuilder.CreateTable(
                name: "Rezervares",
                columns: table => new
                {
                    ID_Rezervare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Client = table.Column<int>(type: "int", nullable: false),
                    ID_Vehicul = table.Column<int>(type: "int", nullable: false),
                    Data_Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Sfarsit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervares", x => x.ID_Rezervare);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    ID_Vehicul = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    An_Fabricatie = table.Column<int>(type: "int", nullable: false),
                    Tip_Combustibil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kilometraj = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.ID_Vehicul);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Mentenantas");

            migrationBuilder.DropTable(
                name: "Rezervares");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
