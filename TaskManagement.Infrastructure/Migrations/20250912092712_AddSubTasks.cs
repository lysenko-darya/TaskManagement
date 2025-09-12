using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TaskEntityId",
                table: "tasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_TaskEntityId",
                table: "tasks",
                column: "TaskEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_tasks_TaskEntityId",
                table: "tasks",
                column: "TaskEntityId",
                principalTable: "tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_tasks_TaskEntityId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_TaskEntityId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "TaskEntityId",
                table: "tasks");
        }
    }
}
