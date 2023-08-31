using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PaymentsCoreApi.Data.Migrations
{
    public partial class add_userdocumentsandvendortable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_type",
                table: "user_logins",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "thirdparty_deposits",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    customer_account = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    transaction_amount = table.Column<double>(type: "double", nullable: false),
                    request_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    network = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    thirdparty_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    channel = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    SystemId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_thirdparty_deposits", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_documents",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    document_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    document_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    document_extension = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    document_path = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
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
                    table.PrimaryKey("PK_user_documents", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vendors",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    vendor_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    vendor_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    system_accountnumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_vendors", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_vendors_vendor_code",
                table: "vendors",
                column: "vendor_code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "thirdparty_deposits");

            migrationBuilder.DropTable(
                name: "user_documents");

            migrationBuilder.DropTable(
                name: "vendors");

            migrationBuilder.DropColumn(
                name: "user_type",
                table: "user_logins");
        }
    }
}
