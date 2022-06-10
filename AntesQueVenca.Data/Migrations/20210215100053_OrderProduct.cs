using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AntesQueVenca.Data.Migrations
{
    public partial class OrderProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Retailer",
                columns: table => new
                {
                    RetailerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedById = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retailer", x => x.RetailerId);
                    table.ForeignKey(
                        name: "FK_Retailer_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Retailer_User_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Retailer_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductItem",
                columns: table => new
                {
                    ProductItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedById = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PriceFrom = table.Column<decimal>(nullable: false),
                    RetailerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItem", x => x.ProductItemId);
                    table.ForeignKey(
                        name: "FK_ProductItem_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductItem_User_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductItem_Retailer_RetailerId",
                        column: x => x.RetailerId,
                        principalTable: "Retailer",
                        principalColumn: "RetailerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrderProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductItemId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => x.OrderProductId);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderProduct_ProductItem_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItem",
                        principalColumn: "ProductItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_OrderId",
                table: "OrderProduct",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductItemId",
                table: "OrderProduct",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItem_CreatedById",
                table: "ProductItem",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItem_LastModifiedById",
                table: "ProductItem",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItem_ProductId",
                table: "ProductItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItem_RetailerId",
                table: "ProductItem",
                column: "RetailerId");

            migrationBuilder.CreateIndex(
                name: "IX_Retailer_CreatedById",
                table: "Retailer",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Retailer_LastModifiedById",
                table: "Retailer",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Retailer_PersonId",
                table: "Retailer",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "ProductItem");

            migrationBuilder.DropTable(
                name: "Retailer");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
