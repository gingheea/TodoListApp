#nullable disable

namespace TodoListApp.Infrastructure.Data.Migrations.TodoListDB
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddNewEntityUserGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_groups",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_groups", x => new { x.user_id, x.group_id });
                    table.ForeignKey(
                        name: "FK_user_groups_group_group_id",
                        column: x => x.group_id,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_groups_group_id",
                table: "user_groups",
                column: "group_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_groups");
        }
    }
}
