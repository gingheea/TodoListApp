#nullable disable

namespace TodoListApp.Services.Migrations.TodoListDB
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InitialTodoListDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by_user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "todo_list",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_list", x => x.id);
                    table.ForeignKey(
                        name: "FK_todo_list_group_group_id",
                        column: x => x.group_id,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "todo_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_completed = table.Column<bool>(type: "bit", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    todo_list_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_todo_item_todo_list_todo_list_id",
                        column: x => x.todo_list_id,
                        principalTable: "todo_list",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_todo_list",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    todo_list_id = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_todo_list", x => new { x.user_id, x.todo_list_id });
                    table.ForeignKey(
                        name: "FK_user_todo_list_todo_list_todo_list_id",
                        column: x => x.todo_list_id,
                        principalTable: "todo_list",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_todo_item_todo_list_id",
                table: "todo_item",
                column: "todo_list_id");

            migrationBuilder.CreateIndex(
                name: "IX_todo_list_group_id",
                table: "todo_list",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_todo_list_todo_list_id",
                table: "user_todo_list",
                column: "todo_list_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "todo_item");

            migrationBuilder.DropTable(
                name: "user_todo_list");

            migrationBuilder.DropTable(
                name: "todo_list");

            migrationBuilder.DropTable(
                name: "group");
        }
    }
}
