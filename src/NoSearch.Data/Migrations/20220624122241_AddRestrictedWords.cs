using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoSearch.Data.Migrations
{
    public partial class AddRestrictedWords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestrictedWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictedWords", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RestrictedWords",
                columns: new[] { "Id", "Reason", "Word" },
                values: new object[] { -2, 0, "fuck" });

            migrationBuilder.InsertData(
                table: "RestrictedWords",
                columns: new[] { "Id", "Reason", "Word" },
                values: new object[] { -1, 0, "shit" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestrictedWords");
        }
    }
}
