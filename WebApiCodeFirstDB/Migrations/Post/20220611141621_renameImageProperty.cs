using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogWebApi.Migrations.Post
{
    public partial class renameImageProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Posts",
                newName: "ImagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Posts",
                newName: "Image");
        }
    }
}
