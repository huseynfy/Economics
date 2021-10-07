using Microsoft.EntityFrameworkCore.Migrations;

namespace Ekonomiks.Migrations
{
    public partial class UpdateALLTABLES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadTime",
                table: "News",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Interviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReadTime",
                table: "Interviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Actuals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReadTime",
                table: "Actuals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadTime",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "ReadTime",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Actuals");

            migrationBuilder.DropColumn(
                name: "ReadTime",
                table: "Actuals");
        }
    }
}
