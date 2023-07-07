using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PaymentsCoreApi.Data.Migrations
{
    public partial class add_countrycode_to_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    product_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    product_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    suspense_account = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    commission_account = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    country_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    created_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    record_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_updated_by = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    last_updated_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    approved_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    approved_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    approved = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_products_product_code",
                table: "products",
                column: "product_code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
