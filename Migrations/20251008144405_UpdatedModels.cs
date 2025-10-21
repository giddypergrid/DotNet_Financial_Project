using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CompanyStocks_CompanyStockId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "CommentIds",
                table: "CompanyStocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyStockId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CompanyStocks_CompanyStockId",
                table: "Comments",
                column: "CompanyStockId",
                principalTable: "CompanyStocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CompanyStocks_CompanyStockId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentIds",
                table: "CompanyStocks");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyStockId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CompanyStocks_CompanyStockId",
                table: "Comments",
                column: "CompanyStockId",
                principalTable: "CompanyStocks",
                principalColumn: "Id");
        }
    }
}
