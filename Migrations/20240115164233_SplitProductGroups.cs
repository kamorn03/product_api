using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApi.Migrations
{
    public partial class SplitProductGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "SubGroupName",
                table: "ProductItems");

            migrationBuilder.AddColumn<int>(
                name: "ProductGroupId",
                table: "ProductItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductSubGroupId",
                table: "ProductItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "productGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GroupName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productGroups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "productSubGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubGroupName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productSubGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productSubGroups_productGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "productGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductGroupId",
                table: "ProductItems",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductSubGroupId",
                table: "ProductItems",
                column: "ProductSubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_productSubGroups_ProductGroupId",
                table: "productSubGroups",
                column: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_productGroups_ProductGroupId",
                table: "ProductItems",
                column: "ProductGroupId",
                principalTable: "productGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_productSubGroups_ProductSubGroupId",
                table: "ProductItems",
                column: "ProductSubGroupId",
                principalTable: "productSubGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_productGroups_ProductGroupId",
                table: "ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_productSubGroups_ProductSubGroupId",
                table: "ProductItems");

            migrationBuilder.DropTable(
                name: "productSubGroups");

            migrationBuilder.DropTable(
                name: "productGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_ProductGroupId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_ProductSubGroupId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "ProductSubGroupId",
                table: "ProductItems");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "ProductItems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SubGroupName",
                table: "ProductItems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
