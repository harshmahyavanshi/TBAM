using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBAM.Migrations
{
    /// <inheritdoc />
    public partial class Db3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Workcentres",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)");

            migrationBuilder.AlterColumn<int>(
                name: "LoginId",
                table: "UserRole",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Workcentre",
                table: "TestBatchItem",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "TestBatchItem",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "TestBatchItem",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "TestBatchItem",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)");

            migrationBuilder.AlterColumn<string>(
                name: "BatchNumber",
                table: "TestBatchItem",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PurposesOfTesting",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "ProductCodes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "ProductCodes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Plants",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Workcentres",
                type: "character varying(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginId",
                table: "UserRole",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Workcentre",
                table: "TestBatchItem",
                type: "character varying(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "TestBatchItem",
                type: "character varying(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "TestBatchItem",
                type: "character varying(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "TestBatchItem",
                type: "character varying(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BatchNumber",
                table: "TestBatchItem",
                type: "character varying(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PurposesOfTesting",
                type: "character varying(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "ProductCodes",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "ProductCodes",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Plants",
                type: "character varying(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
