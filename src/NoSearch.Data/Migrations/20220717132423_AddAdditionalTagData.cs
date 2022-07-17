using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSearch.Data.Migrations
{
    public partial class AddAdditionalTagData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsRestricted", "Name", "ResourceId" },
                values: new object[] { -8, false, "Dissertation / White Paper", null });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsRestricted", "Name", "ResourceId" },
                values: new object[] { -7, false, "Online Games", null });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsRestricted", "Name", "ResourceId" },
                values: new object[] { -6, false, "Online Tools", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: -6);
        }
    }
}
