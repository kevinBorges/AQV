using Microsoft.EntityFrameworkCore.Migrations;

namespace AntesQueVenca.Data.Migrations
{
    public partial class RetailerProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RetailerId",
                table: "ProductItem",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RetailerId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RetailerProductItem",
                columns: table => new
                {
                    RetailerProductItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetailerId = table.Column<int>(nullable: false),
                    ProductItemId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailerProductItem", x => x.RetailerProductItemId);
                    table.ForeignKey(
                        name: "FK_RetailerProductItem_ProductItem_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItem",
                        principalColumn: "ProductItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailerProductItem_Retailer_RetailerId",
                        column: x => x.RetailerId,
                        principalTable: "Retailer",
                        principalColumn: "RetailerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RetailerId",
                table: "Orders",
                column: "RetailerId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailerProductItem_ProductItemId",
                table: "RetailerProductItem",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailerProductItem_RetailerId",
                table: "RetailerProductItem",
                column: "RetailerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Retailer_RetailerId",
                table: "Orders",
                column: "RetailerId",
                principalTable: "Retailer",
                principalColumn: "RetailerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Retailer_RetailerId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "RetailerProductItem");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RetailerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RetailerId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "RetailerId",
                table: "ProductItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
