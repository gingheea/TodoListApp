#nullable disable

namespace TodoListApp.Infrastructure.Data.Migrations.TodoListDB
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UpdateOnModelCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todo_list_group_group_id",
                table: "todo_list");

            migrationBuilder.AddForeignKey(
                name: "FK_todo_list_group_group_id",
                table: "todo_list",
                column: "group_id",
                principalTable: "group",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todo_list_group_group_id",
                table: "todo_list");

            migrationBuilder.AddForeignKey(
                name: "FK_todo_list_group_group_id",
                table: "todo_list",
                column: "group_id",
                principalTable: "group",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
