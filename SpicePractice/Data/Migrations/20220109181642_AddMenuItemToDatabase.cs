using Microsoft.EntityFrameworkCore.Migrations;

namespace SpicePractice.Data.Migrations
{
    public partial class AddMenuItemToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManuItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Spicyness = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManuItem_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ManuItem_SubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManuItem_CategoryId",
                table: "ManuItem",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ManuItem_SubCategoryId",
                table: "ManuItem",
                column: "SubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManuItem");
        }
    }
}
