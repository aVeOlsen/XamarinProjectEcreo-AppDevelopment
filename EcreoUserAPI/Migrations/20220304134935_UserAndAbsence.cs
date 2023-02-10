using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcreoUserAPI.Migrations
{
    public partial class UserAndAbsence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    BaseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminStatus = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CurrentAbsenceStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.BaseID);
                });

            migrationBuilder.CreateTable(
                name: "AbsenceStatuses",
                columns: table => new
                {
                    BaseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AbsenceStatusRole = table.Column<int>(type: "int", nullable: false),
                    FromeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserBaseID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceStatuses", x => x.BaseID);
                    table.ForeignKey(
                        name: "FK_AbsenceStatuses_Users_UserBaseID",
                        column: x => x.UserBaseID,
                        principalTable: "Users",
                        principalColumn: "BaseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceStatuses_UserBaseID",
                table: "AbsenceStatuses",
                column: "UserBaseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbsenceStatuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
