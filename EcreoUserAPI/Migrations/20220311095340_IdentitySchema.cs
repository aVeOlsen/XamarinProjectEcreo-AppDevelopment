using Microsoft.EntityFrameworkCore.Migrations;

namespace EcreoUserAPI.Migrations
{
    public partial class IdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonalInformationBaseID",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactInformations",
                columns: table => new
                {
                    BaseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformations", x => x.BaseID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalInformationBaseID",
                table: "Users",
                column: "PersonalInformationBaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ContactInformations_PersonalInformationBaseID",
                table: "Users",
                column: "PersonalInformationBaseID",
                principalTable: "ContactInformations",
                principalColumn: "BaseID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ContactInformations_PersonalInformationBaseID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ContactInformations");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonalInformationBaseID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PersonalInformationBaseID",
                table: "Users");
        }
    }
}
