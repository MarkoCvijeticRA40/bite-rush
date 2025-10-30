#pragma warning disable IDE0005 // Using directive is unnecessary.
using System;
#pragma warning restore IDE0005 // Using directive is unnecessary.
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable IDE0161 // Convert to file-scoped namespace
namespace Infrastructure.Migrations
#pragma warning restore IDE0161 // Convert to file-scoped namespace
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "registration_date_time",
                schema: "public",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "registration_date_time",
                schema: "public",
                table: "users");
        }
    }
}
