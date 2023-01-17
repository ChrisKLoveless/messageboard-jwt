using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageBoard.Migrations
{
    public partial class AddSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "ThreadsId", "Title", "UserId" },
                values: new object[] { 1, "News", 1 });

            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "ThreadsId", "Title", "UserId" },
                values: new object[] { 2, "Memes", 2 });

            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "ThreadsId", "Title", "UserId" },
                values: new object[] { 3, "Sports", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "ThreadsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "ThreadsId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "ThreadsId",
                keyValue: 3);
        }
    }
}
