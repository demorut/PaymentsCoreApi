using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PaymentsCoreApi.Data.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    account_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    account_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    account_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    currency_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    account_status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    balance = table.Column<double>(type: "double", nullable: false),
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
                    table.PrimaryKey("PK_accounts", x => x.account_number);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "agents_signup_requests",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    agent_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    agent_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    contact_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    agent_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    agent_status = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    country_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    street = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    id_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    id_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    documents = table.Column<string>(type: "longtext", nullable: true),
                    comment = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_agents_signup_requests", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "channels",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    channel_key = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    channel_secretKey = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_channels", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    country_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    country_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    currency_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    currency = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_Country", x => x.country_code);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    customer_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    customer_status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    country_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    user_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "general_ledger",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    account_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    transaction_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    debit_amount = table.Column<double>(type: "double", nullable: false),
                    credit_amount = table.Column<double>(type: "double", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    partner_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    system_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    transaction_header_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    naration = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    channel = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    transaction_category = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    balance = table.Column<double>(type: "double", nullable: false),
                    customer_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general_ledger", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "password_reset_requests",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    status = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    otp = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_password_reset_requests", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "signup_requests",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    customer_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    status = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    country_code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    otp = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    rand_code = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
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
                    table.PrimaryKey("PK_signup_requests", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transaction_header",
                columns: table => new
                {
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    from_account = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    from_customer_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    to_account = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    to_customer_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    transaction_amount = table.Column<double>(type: "double", nullable: false),
                    transaction_header_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    partner_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    channel = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ledger_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    product_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    transaction_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    partner = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_transaction_header", x => x.record_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    Username = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    record_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    rand_code = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    reset = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    last_password_change_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_login_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    login_attempts = table.Column<int>(type: "int", nullable: false),
                    reset_password = table.Column<bool>(type: "tinyint(1)", nullable: false),
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
                    table.PrimaryKey("PK_user_logins", x => x.Username);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "agents_signup_requests");

            migrationBuilder.DropTable(
                name: "channels");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "general_ledger");

            migrationBuilder.DropTable(
                name: "password_reset_requests");

            migrationBuilder.DropTable(
                name: "signup_requests");

            migrationBuilder.DropTable(
                name: "transaction_header");

            migrationBuilder.DropTable(
                name: "user_logins");
        }
    }
}
