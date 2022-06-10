using Microsoft.EntityFrameworkCore.Migrations;

namespace AntesQueVenca.Data.Migrations
{
    public partial class RetailerProductt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItem_Retailer_RetailerId",
                table: "ProductItem");

            migrationBuilder.DropIndex(
                name: "IX_ProductItem_RetailerId",
                table: "ProductItem");

            migrationBuilder.DropColumn(
                name: "RetailerId",
                table: "ProductItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RetailerId",
                table: "ProductItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductItem_RetailerId",
                table: "ProductItem",
                column: "RetailerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItem_Retailer_RetailerId",
                table: "ProductItem",
                column: "RetailerId",
                principalTable: "Retailer",
                principalColumn: "RetailerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
