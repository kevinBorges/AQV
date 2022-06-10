using Microsoft.EntityFrameworkCore.Migrations;

namespace AntesQueVenca.Data.Migrations
{
    public partial class PessoaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Person_PersonId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "User",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Person_PersonId",
                table: "User",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Person_PersonId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "User",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Person_PersonId",
                table: "User",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
