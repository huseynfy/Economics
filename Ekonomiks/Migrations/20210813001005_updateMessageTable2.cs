using Microsoft.EntityFrameworkCore.Migrations;

namespace Ekonomiks.Migrations
{
    public partial class updateMessageTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Messages",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
