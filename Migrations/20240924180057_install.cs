using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoApp.Migrations
{
    /// <inheritdoc />
    public partial class install : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "StatusSet",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusSet", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "ToDoSet",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoSet_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDoSet_StatusSet_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusSet",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { "hobby", "Hobby" },
                    { "home", "Home" },
                    { "work", "Work" }
                });

            migrationBuilder.InsertData(
                table: "StatusSet",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { "done", "Done" },
                    { "open", "Open" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoSet_CategoryId",
                table: "ToDoSet",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoSet_StatusId",
                table: "ToDoSet",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoSet");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "StatusSet");
        }
    }
}
