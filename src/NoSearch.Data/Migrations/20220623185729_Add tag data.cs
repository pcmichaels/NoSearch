using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSearch.Data.Migrations
{
    public partial class Addtagdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsRestricted", "Name", "ResourceId" },
                values: new object[,]
                {
                    { -5, false, "Video", null },
                    { -4, false, "Tutorial", null },
                    { -3, false, "Programming", null },
                    { -2, false, "News", null },
                    { -1, false, "Blog", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
