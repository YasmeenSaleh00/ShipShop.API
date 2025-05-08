using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "MainImage");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "MainImage",
                table: "Products",
                newName: "ImageUrl");
        }
    }
}
