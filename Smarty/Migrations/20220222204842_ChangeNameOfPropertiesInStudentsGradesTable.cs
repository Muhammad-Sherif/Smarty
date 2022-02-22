using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class ChangeNameOfPropertiesInStudentsGradesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsGrades_CoursesGrades_GradeName_CourseId",
                table: "StudentsGrades");

            migrationBuilder.RenameColumn(
                name: "GradeValue",
                table: "StudentsGrades",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "GradeName",
                table: "StudentsGrades",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsGrades_GradeName_CourseId",
                table: "StudentsGrades",
                newName: "IX_StudentsGrades_Name_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsGrades_CoursesGrades_Name_CourseId",
                table: "StudentsGrades",
                columns: new[] { "Name", "CourseId" },
                principalTable: "CoursesGrades",
                principalColumns: new[] { "Name", "CourseId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsGrades_CoursesGrades_Name_CourseId",
                table: "StudentsGrades");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "StudentsGrades",
                newName: "GradeValue");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StudentsGrades",
                newName: "GradeName");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsGrades_Name_CourseId",
                table: "StudentsGrades",
                newName: "IX_StudentsGrades_GradeName_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsGrades_CoursesGrades_GradeName_CourseId",
                table: "StudentsGrades",
                columns: new[] { "GradeName", "CourseId" },
                principalTable: "CoursesGrades",
                principalColumns: new[] { "Name", "CourseId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
