using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageBoard.Migrations
{
    public partial class AddEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThreadId",
                table: "Threads",
                newName: "ThreadsId");

            migrationBuilder.RenameColumn(
                name: "ThreadId",
                table: "Posts",
                newName: "ThreadsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThreadsId",
                table: "Threads",
                newName: "ThreadId");

            migrationBuilder.RenameColumn(
                name: "ThreadsId",
                table: "Posts",
                newName: "ThreadId");
        }
    }
}
