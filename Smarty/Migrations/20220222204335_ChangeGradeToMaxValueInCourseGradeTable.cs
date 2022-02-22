using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class ChangeGradeToMaxValueInCourseGradeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "CoursesGrades",
                newName: "MaxValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxValue",
                table: "CoursesGrades",
                newName: "Grade");
        }
    }
}
