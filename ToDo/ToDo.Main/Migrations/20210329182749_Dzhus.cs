using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class Dzhus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dzhus",
                table: "ToDoItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dzhus",
                table: "ToDoItems");
        }
    }
}
