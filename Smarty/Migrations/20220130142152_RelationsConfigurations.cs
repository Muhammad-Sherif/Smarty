using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class RelationsConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StudentsCourses",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsCourses", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Members_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentsLabs",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LabName = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsLabs", x => new { x.StudentId, x.CourseId, x.LabName });
                    table.ForeignKey(
                        name: "FK_StudentsLabs_Labs_LabName_CourseId",
                        columns: x => new { x.LabName, x.CourseId },
                        principalTable: "Labs",
                        principalColumns: new[] { "Name", "CourseId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsLabs_Members_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Labs_CourseId",
                table: "Labs",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesGrades_CourseId",
                table: "CoursesGrades",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCourses_CourseId",
                table: "StudentsCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsLabs_LabName_CourseId",
                table: "StudentsLabs",
                columns: new[] { "LabName", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Members_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesGrades_Courses_CourseId",
                table: "CoursesGrades",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Labs_Courses_CourseId",
                table: "Labs",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Members_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesGrades_Courses_CourseId",
                table: "CoursesGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_Labs_Courses_CourseId",
                table: "Labs");

            migrationBuilder.DropTable(
                name: "StudentsCourses");

            migrationBuilder.DropTable(
                name: "StudentsLabs");

            migrationBuilder.DropIndex(
                name: "IX_Labs_CourseId",
                table: "Labs");

            migrationBuilder.DropIndex(
                name: "IX_CoursesGrades_CourseId",
                table: "CoursesGrades");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Courses");
        }
    }
}
