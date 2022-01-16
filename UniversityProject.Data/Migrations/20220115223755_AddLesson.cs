using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProject.Data.Migrations
{
    public partial class AddLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "register_date",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 15, 22, 37, 55, 21, DateTimeKind.Utc).AddTicks(8639),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 15, 19, 15, 21, 934, DateTimeKind.Utc).AddTicks(5630));

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "register_date",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 15, 19, 15, 21, 934, DateTimeKind.Utc).AddTicks(5630),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 15, 22, 37, 55, 21, DateTimeKind.Utc).AddTicks(8639));
        }
    }
}
