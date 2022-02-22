using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class AddRelationBetweenCoursesGradesAndStudentTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsGrades",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    GradeName = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GradeValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsGrades", x => new { x.StudentId, x.CourseId, x.GradeName });
                    table.ForeignKey(
                        name: "FK_StudentsGrades_CoursesGrades_GradeName_CourseId",
                        columns: x => new { x.GradeName, x.CourseId },
                        principalTable: "CoursesGrades",
                        principalColumns: new[] { "Name", "CourseId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsGrades_Members_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsGrades_GradeName_CourseId",
                table: "StudentsGrades",
                columns: new[] { "GradeName", "CourseId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsGrades");
        }
    }
}
