using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBAM.Migrations
{
    /// <inheritdoc />
    public partial class d2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCodes",
                table: "ProductCodes");

            migrationBuilder.RenameTable(
                name: "ProductCodes",
                newName: "ProductMaster");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaster",
                table: "ProductMaster",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaster",
                table: "ProductMaster");

            migrationBuilder.RenameTable(
                name: "ProductMaster",
                newName: "ProductCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCodes",
                table: "ProductCodes",
                column: "Id");
        }
    }
}
