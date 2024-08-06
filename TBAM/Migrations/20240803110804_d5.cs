using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBAM.Migrations
{
    /// <inheritdoc />
    public partial class d5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Refno",
                table: "TestBatchItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Refno",
                table: "TestBatch",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Refno",
                table: "TestBatchItem");

            migrationBuilder.DropColumn(
                name: "Refno",
                table: "TestBatch");
        }
    }
}
