using Microsoft.EntityFrameworkCore.Migrations;

namespace WriteFlow.DataLayer.Migrations
{
    public partial class Add_ShortDescription_TO_PostEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Posts",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Posts");
        }
    }
}
