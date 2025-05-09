using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removefromcustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_CustomerId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_CustomerId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "CodeExpiry",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CodeExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CustomerId",
                table: "Messages",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_CustomerId",
                table: "Messages",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
