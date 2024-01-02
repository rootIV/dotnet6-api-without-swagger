using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class product_require_in_tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Products_ProductCode",
                table: "Tag");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCode",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Products_ProductCode",
                table: "Tag",
                column: "ProductCode",
                principalTable: "Products",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Products_ProductCode",
                table: "Tag");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCode",
                table: "Tag",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Products_ProductCode",
                table: "Tag",
                column: "ProductCode",
                principalTable: "Products",
                principalColumn: "Code");
        }
    }
}
