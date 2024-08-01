using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBAM.Migrations
{
    /// <inheritdoc />
    public partial class Db2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurposesOfTesting",
                table: "TestBatch");

            migrationBuilder.RenameColumn(
                name: "Plant",
                table: "TestBatch",
                newName: "PurposesOfTestingId");

            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "TestBatch",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "TestBatch");

            migrationBuilder.RenameColumn(
                name: "PurposesOfTestingId",
                table: "TestBatch",
                newName: "Plant");

            migrationBuilder.AddColumn<string>(
                name: "PurposesOfTesting",
                table: "TestBatch",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
