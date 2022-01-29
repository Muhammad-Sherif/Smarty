using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class CreateCoursesGradesAndLabsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursesGrades",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesGrades", x => new { x.Name, x.CourseId });
                });

            migrationBuilder.CreateTable(
                name: "Labs",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labs", x => new { x.Name, x.CourseId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesGrades");

            migrationBuilder.DropTable(
                name: "Labs");
        }
    }
}
