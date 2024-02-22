using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectPrezente.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLASSES",
                columns: table => new
                {
                    CLASS_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CLASS_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CLASS_TYPE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLASSES", x => x.CLASS_ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    USER_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_TYPE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "ATTENDANCES",
                columns: table => new
                {
                    ATTENDANCE_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_PRESENT = table.Column<bool>(type: "bit", nullable: false),
                    CLASS_ID = table.Column<long>(type: "bigint", nullable: false),
                    STUDENT_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTENDANCES", x => x.ATTENDANCE_ID);
                    table.ForeignKey(
                        name: "FK_ATTENDANCES_CLASSES_CLASS_ID",
                        column: x => x.CLASS_ID,
                        principalTable: "CLASSES",
                        principalColumn: "CLASS_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATTENDANCES_USERS_STUDENT_ID",
                        column: x => x.STUDENT_ID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLASS_TEACHERS",
                columns: table => new
                {
                    CLASS_ID = table.Column<long>(type: "bigint", nullable: false),
                    PROFESSOR_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLASS_TEACHERS", x => new { x.CLASS_ID, x.PROFESSOR_ID });
                    table.ForeignKey(
                        name: "FK_CLASS_TEACHERS_CLASSES_CLASS_ID",
                        column: x => x.CLASS_ID,
                        principalTable: "CLASSES",
                        principalColumn: "CLASS_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CLASS_TEACHERS_USERS_PROFESSOR_ID",
                        column: x => x.PROFESSOR_ID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ENROLLMENTS",
                columns: table => new
                {
                    STUDENT_ID = table.Column<long>(type: "bigint", nullable: false),
                    CLASS_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENROLLMENTS", x => new { x.STUDENT_ID, x.CLASS_ID });
                    table.ForeignKey(
                        name: "FK_ENROLLMENTS_CLASSES_CLASS_ID",
                        column: x => x.CLASS_ID,
                        principalTable: "CLASSES",
                        principalColumn: "CLASS_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ENROLLMENTS_USERS_STUDENT_ID",
                        column: x => x.STUDENT_ID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_DETAILS",
                columns: table => new
                {
                    DETAILS_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GENDER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_DETAILS", x => x.DETAILS_ID);
                    table.ForeignKey(
                        name: "FK_USER_DETAILS_USERS_UserID",
                        column: x => x.UserID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDANCES_CLASS_ID",
                table: "ATTENDANCES",
                column: "CLASS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDANCES_STUDENT_ID",
                table: "ATTENDANCES",
                column: "STUDENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CLASS_TEACHERS_PROFESSOR_ID",
                table: "CLASS_TEACHERS",
                column: "PROFESSOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ENROLLMENTS_CLASS_ID",
                table: "ENROLLMENTS",
                column: "CLASS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_DETAILS_UserID",
                table: "USER_DETAILS",
                column: "UserID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATTENDANCES");

            migrationBuilder.DropTable(
                name: "CLASS_TEACHERS");

            migrationBuilder.DropTable(
                name: "ENROLLMENTS");

            migrationBuilder.DropTable(
                name: "USER_DETAILS");

            migrationBuilder.DropTable(
                name: "CLASSES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
