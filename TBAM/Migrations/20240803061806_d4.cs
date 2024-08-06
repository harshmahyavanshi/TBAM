using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBAM.Migrations
{
    /// <inheritdoc />
    public partial class d4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Workcentres",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TestBatchItem",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TestBatch",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Role",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurposesOfTesting",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProductMaster",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Plants",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Workcentres",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TestBatchItem",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TestBatch",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Role",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurposesOfTesting",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProductMaster",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Plants",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);
        }
    }
}
