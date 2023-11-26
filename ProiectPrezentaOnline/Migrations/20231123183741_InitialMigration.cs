using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectPrezentaOnline.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursuri",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursuri", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Laboratoare",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratoare", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utilizatori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeUtilizator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumarDeTelefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizatori", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrezenteCursuri",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDStudent = table.Column<int>(type: "int", nullable: true),
                    IDCurs = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prezent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrezenteCursuri", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PrezenteCursuri_Cursuri_IDCurs",
                        column: x => x.IDCurs,
                        principalTable: "Cursuri",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PrezenteCursuri_Utilizatori_IDStudent",
                        column: x => x.IDStudent,
                        principalTable: "Utilizatori",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrezenteLaboratoare",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDStudent = table.Column<int>(type: "int", nullable: true),
                    IDLaborator = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prezent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrezenteLaboratoare", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PrezenteLaboratoare_Laboratoare_IDLaborator",
                        column: x => x.IDLaborator,
                        principalTable: "Laboratoare",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PrezenteLaboratoare_Utilizatori_IDStudent",
                        column: x => x.IDStudent,
                        principalTable: "Utilizatori",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrezenteCursuri_IDCurs",
                table: "PrezenteCursuri",
                column: "IDCurs");

            migrationBuilder.CreateIndex(
                name: "IX_PrezenteCursuri_IDStudent",
                table: "PrezenteCursuri",
                column: "IDStudent");

            migrationBuilder.CreateIndex(
                name: "IX_PrezenteLaboratoare_IDLaborator",
                table: "PrezenteLaboratoare",
                column: "IDLaborator");

            migrationBuilder.CreateIndex(
                name: "IX_PrezenteLaboratoare_IDStudent",
                table: "PrezenteLaboratoare",
                column: "IDStudent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrezenteCursuri");

            migrationBuilder.DropTable(
                name: "PrezenteLaboratoare");

            migrationBuilder.DropTable(
                name: "Cursuri");

            migrationBuilder.DropTable(
                name: "Laboratoare");

            migrationBuilder.DropTable(
                name: "Utilizatori");
        }
    }
}
