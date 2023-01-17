using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageBoard.Migrations
{
    public partial class AddPostSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Body", "ThreadsId", "UserId" },
                values: new object[] { 1, "News", 1, 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Body", "ThreadsId", "UserId" },
                values: new object[] { 2, "Memes", 2, 2 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Body", "ThreadsId", "UserId" },
                values: new object[] { 3, "Sports", 3, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3);
        }
    }
}
