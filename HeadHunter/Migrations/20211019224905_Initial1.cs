using Microsoft.EntityFrameworkCore.Migrations;

namespace HeadHunter.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompany",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Occupation",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompany",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
