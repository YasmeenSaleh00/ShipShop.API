using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addsomeseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Customer");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmPassword", "Discriminator", "Email", "FirstName", "IsActive", "LastName", "Password", "RoleId", "UpdatedOn" },
                values: new object[] { 1, "12345678", "Admin", "yasmeensaleh147@gmail.com", "Yasmeen", true, "Saleh", "12345678", 1, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Read");
        }
    }
}
