using Microsoft.EntityFrameworkCore.Migrations;

namespace AntesQueVenca.Data.Migrations
{
    public partial class city : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UF",
                table: "Address",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UF",
                table: "Address");
        }
    }
}
