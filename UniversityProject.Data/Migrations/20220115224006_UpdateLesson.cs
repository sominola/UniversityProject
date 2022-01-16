using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Data.Migrations
{
    public partial class UpdateLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "Lesson");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lesson",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lesson",
                newName: "id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "register_date",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 15, 22, 40, 5, 590, DateTimeKind.Utc).AddTicks(609),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 15, 22, 37, 55, 21, DateTimeKind.Utc).AddTicks(8639));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Lesson",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson",
                column: "id");

            migrationBuilder.CreateTable(
                name: "UserLesson",
                columns: table => new
                {
                    LessonsId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLesson", x => new { x.LessonsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserLesson_Lesson_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lesson",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLesson_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLesson_UsersId",
                table: "UserLesson",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson");

            migrationBuilder.RenameTable(
                name: "Lesson",
                newName: "Lessons");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Lessons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Lessons",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "register_date",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 15, 22, 37, 55, 21, DateTimeKind.Utc).AddTicks(8639),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 15, 22, 40, 5, 590, DateTimeKind.Utc).AddTicks(609));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");
        }
    }
}
