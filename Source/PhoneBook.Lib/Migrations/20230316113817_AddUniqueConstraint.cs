using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.Lib.Migrations
{
    public partial class AddUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "PhoneBook",
                table: "Company",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Name",
                schema: "PhoneBook",
                table: "Company",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Company_Name",
                schema: "PhoneBook",
                table: "Company");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "PhoneBook",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
