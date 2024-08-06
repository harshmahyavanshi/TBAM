using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBAM.Migrations
{
    /// <inheritdoc />
    public partial class d3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Workcentres",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Workcentres",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Workcentres",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Workcentres",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Workcentres",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Workcentres",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Workcentres",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TestBatchItem",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TestBatch",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Role",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PurposesOfTesting",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PurposesOfTesting",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PurposesOfTesting",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "PurposesOfTesting",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PurposesOfTesting",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "PurposesOfTesting",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "PurposesOfTesting",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductMaster",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ProductMaster",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ProductMaster",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "ProductMaster",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductMaster",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "ProductMaster",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "ProductMaster",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Plants",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Plants",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Plants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Plants",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Plants",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Plants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Plants",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Workcentres");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Workcentres");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Workcentres");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Workcentres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Workcentres");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Workcentres");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Workcentres");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PurposesOfTesting");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PurposesOfTesting");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PurposesOfTesting");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PurposesOfTesting");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PurposesOfTesting");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "PurposesOfTesting");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PurposesOfTesting");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProductMaster");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductMaster");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ProductMaster");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ProductMaster");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductMaster");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "ProductMaster");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ProductMaster");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Plants");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TestBatchItem",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TestBatch",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Role",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
