using Microsoft.EntityFrameworkCore.Migrations;

namespace AntesQueVenca.Data.Migrations
{
    public partial class ordertype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Orders");
        }
    }
}
