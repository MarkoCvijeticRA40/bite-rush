
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Product_Price_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                schema: "public",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "price_id",
                schema: "public",
                table: "products",
                newName: "product_price_id");

#pragma warning disable S4581 // "new Guid()" should not be used
            migrationBuilder.AddColumn<Guid>(
                name: "product_price_id1",
                schema: "public",
                table: "products",
                type: "uuid",
                nullable: true);
#pragma warning restore S4581 // "new Guid()" should not be used

#pragma warning disable IDE0053 // Use expression body for lambda expression
            migrationBuilder.CreateTable(
                name: "product_prices",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_prices", x => x.id);
                });
#pragma warning restore IDE0053 // Use expression body for lambda expression

            migrationBuilder.CreateIndex(
                name: "ix_products_product_price_id1",
                schema: "public",
                table: "products",
                column: "product_price_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_products_product_prices_product_price_id1",
                schema: "public",
                table: "products",
                column: "product_price_id1",
                principalSchema: "public",
                principalTable: "product_prices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_product_prices_product_price_id1",
                schema: "public",
                table: "products");

            migrationBuilder.DropTable(
                name: "product_prices",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "ix_products_product_price_id1",
                schema: "public",
                table: "products");

            migrationBuilder.DropColumn(
                name: "product_price_id1",
                schema: "public",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "product_price_id",
                schema: "public",
                table: "products",
                newName: "price_id");

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
