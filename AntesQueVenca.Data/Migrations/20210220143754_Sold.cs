using Microsoft.EntityFrameworkCore.Migrations;

namespace AntesQueVenca.Data.Migrations
{
    public partial class Sold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Sold",
                table: "ProductItem",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sold",
                table: "ProductItem");
        }
    }
}
