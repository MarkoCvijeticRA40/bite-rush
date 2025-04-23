using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Change_Product_Price_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_product_prices_product_price_id1",
                schema: "public",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_products_product_price_id1",
                schema: "public",
                table: "products");

            migrationBuilder.DropColumn(
                name: "product_price_id",
                schema: "public",
                table: "products");

            migrationBuilder.DropColumn(
                name: "product_price_id1",
                schema: "public",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "amount",
                schema: "public",
                table: "product_prices",
                newName: "unit_amount");

            migrationBuilder.AddColumn<bool>(
                name: "live_mode",
                schema: "public",
                table: "products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "active",
                schema: "public",
                table: "product_prices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "billing_scheme",
                schema: "public",
                table: "product_prices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "currency",
                schema: "public",
                table: "product_prices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "livemode",
                schema: "public",
                table: "product_prices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "product_id",
                schema: "public",
                table: "product_prices",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unit_amount_decimal",
                schema: "public",
                table: "product_prices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_product_prices_product_id",
                schema: "public",
                table: "product_prices",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_prices_products_product_id",
                schema: "public",
                table: "product_prices",
                column: "product_id",
                principalSchema: "public",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_prices_products_product_id",
                schema: "public",
                table: "product_prices");

            migrationBuilder.DropIndex(
                name: "ix_product_prices_product_id",
                schema: "public",
                table: "product_prices");

            migrationBuilder.DropColumn(
                name: "live_mode",
                schema: "public",
                table: "products");

            migrationBuilder.DropColumn(
                name: "active",
                schema: "public",
                table: "product_prices");

            migrationBuilder.DropColumn(
                name: "billing_scheme",
                schema: "public",
                table: "product_prices");

            migrationBuilder.DropColumn(
                name: "currency",
                schema: "public",
                table: "product_prices");

            migrationBuilder.DropColumn(
                name: "livemode",
                schema: "public",
                table: "product_prices");

            migrationBuilder.DropColumn(
                name: "product_id",
                schema: "public",
                table: "product_prices");

            migrationBuilder.DropColumn(
                name: "unit_amount_decimal",
                schema: "public",
                table: "product_prices");

            migrationBuilder.RenameColumn(
                name: "unit_amount",
                schema: "public",
                table: "product_prices",
                newName: "amount");

            migrationBuilder.AddColumn<string>(
                name: "product_price_id",
                schema: "public",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "product_price_id1",
                schema: "public",
                table: "products",
                type: "uuid",
                nullable: true);

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
    }
}
