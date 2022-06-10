using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AntesQueVenca.Data.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumer",
                columns: table => new
                {
                    ConsumerId = table.Column<int>(nullable: false)
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
                    table.PrimaryKey("PK_Consumer", x => x.ConsumerId);
                    table.ForeignKey(
                        name: "FK_Consumer_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consumer_User_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consumer_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedById = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    ConsumerId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    WithdrawDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Consumer_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumer",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_User_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_CreatedById",
                table: "Consumer",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_LastModifiedById",
                table: "Consumer",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_PersonId",
                table: "Consumer",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ConsumerId",
                table: "Orders",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedById",
                table: "Orders",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LastModifiedById",
                table: "Orders",
                column: "LastModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Consumer");
        }
    }
}
