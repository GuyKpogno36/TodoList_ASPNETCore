using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_List_ASPNETCore.Migrations
{
    public partial class AddCategoryEntityAndUpdateTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_TASK_CATEGORY_Category_ID", table: "TASK");

            migrationBuilder.DropTable(
                name: "CATEGORY");

            migrationBuilder.CreateTable(
                name: "CATEGORY",
                columns: table => new
                {
                    Category_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY", x => x.Category_ID);
                });

            migrationBuilder.DropTable(
                name: "TASK");

            migrationBuilder.CreateTable(
                name: "TASK",
                columns: table => new
                {
                    Task_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Task_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Task_Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_Status = table.Column<bool>(type: "bit", nullable: false),
                    Category_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK", x => x.Task_ID);
                    table.ForeignKey(
                        name: "FK_TASK_CATEGORY_Category_ID",
                        column: x => x.Category_ID,
                        principalTable: "CATEGORY",
                        principalColumn: "Category_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TASK_Category_ID",
                table: "TASK",
                column: "Category_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TASK");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "CATEGORY");
        }
    }
}
