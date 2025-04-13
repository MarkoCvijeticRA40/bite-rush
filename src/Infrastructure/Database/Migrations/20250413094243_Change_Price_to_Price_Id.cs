using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Change_Price_to_Price_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                schema: "public",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "price_id",
                schema: "public",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price_id",
                schema: "public",
                table: "products");

            migrationBuilder.AddColumn<long>(
                name: "price",
                schema: "public",
                table: "products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
