using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_tasks_TaskEntityId",
                table: "tasks");

            migrationBuilder.RenameColumn(
                name: "TaskEntityId",
                table: "tasks",
                newName: "ParentTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_TaskEntityId",
                table: "tasks",
                newName: "IX_tasks_ParentTaskId");

            migrationBuilder.CreateTable(
                name: "TaskRelation",
                columns: table => new
                {
                    TaskId = table.Column<long>(type: "bigint", nullable: false),
                    RelatedTaskId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskRelation", x => new { x.RelatedTaskId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TaskRelation_tasks_RelatedTaskId",
                        column: x => x.RelatedTaskId,
                        principalTable: "tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskRelation_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskRelation_TaskId",
                table: "TaskRelation",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_tasks_ParentTaskId",
                table: "tasks",
                column: "ParentTaskId",
                principalTable: "tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_tasks_ParentTaskId",
                table: "tasks");

            migrationBuilder.DropTable(
                name: "TaskRelation");

            migrationBuilder.RenameColumn(
                name: "ParentTaskId",
                table: "tasks",
                newName: "TaskEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_ParentTaskId",
                table: "tasks",
                newName: "IX_tasks_TaskEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_tasks_TaskEntityId",
                table: "tasks",
                column: "TaskEntityId",
                principalTable: "tasks",
                principalColumn: "Id");
        }
    }
}
