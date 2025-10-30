using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable IDE0161 // Convert to file-scoped namespace
namespace Infrastructure.Migrations
#pragma warning restore IDE0161 // Convert to file-scoped namespace
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "products",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    updated = table.Column<long>(type: "bigint", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    live_mode = table.Column<bool>(type: "boolean", nullable: false),
                    images = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_prices",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    billing_scheme = table.Column<string>(type: "text", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    livemode = table.Column<bool>(type: "boolean", nullable: false),
                    product_id = table.Column<string>(type: "text", nullable: false),
                    unit_amount = table.Column<long>(type: "bigint", nullable: true),
                    unit_amount_decimal = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_prices", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_prices_products_product_id",
                        column: x => x.product_id,
                        principalSchema: "public",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_prices_product_id",
                schema: "public",
                table: "product_prices",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "public",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_prices",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "products",
                schema: "public");
        }
    }
}
