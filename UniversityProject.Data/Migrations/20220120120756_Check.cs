using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Data.Migrations
{
    public partial class Check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLesson");

            migrationBuilder.AlterColumn<DateTime>(
                name: "register_date",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 15, 22, 40, 5, 590, DateTimeKind.Utc).AddTicks(609));

            migrationBuilder.CreateTable(
                name: "LessonTeacher",
                columns: table => new
                {
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTeacher", x => new { x.LessonId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_LessonTeacher_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonTeacher_User_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonUser",
                columns: table => new
                {
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUser", x => new { x.LessonId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_LessonUser_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonUser_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "id",
                keyValue: 1L,
                column: "name",
                value: "Student");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "Teacher");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "id", "name" },
                values: new object[] { 3L, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_LessonTeacher_TeacherId",
                table: "LessonTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUser_StudentId",
                table: "LessonUser",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonTeacher");

            migrationBuilder.DropTable(
                name: "LessonUser");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "register_date",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 15, 22, 40, 5, 590, DateTimeKind.Utc).AddTicks(609),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

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

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "id",
                keyValue: 1L,
                column: "name",
                value: "User");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "Admin");

            migrationBuilder.CreateIndex(
                name: "IX_UserLesson_UsersId",
                table: "UserLesson",
                column: "UsersId");
        }
    }
}
