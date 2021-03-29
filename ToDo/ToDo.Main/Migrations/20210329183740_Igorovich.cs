using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class Igorovich : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Igorovich",
                table: "ToDoItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Igorovich",
                table: "ToDoItems");
        }
    }
}
