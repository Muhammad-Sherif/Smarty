using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class ChangeRelationsDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Members_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Members_StudentId",
                table: "StudentsCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsLabs_Members_StudentId",
                table: "StudentsLabs");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Members_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Members_StudentId",
                table: "StudentsCourses",
                column: "StudentId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsLabs_Members_StudentId",
                table: "StudentsLabs",
                column: "StudentId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Members_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsCourses_Members_StudentId",
                table: "StudentsCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsLabs_Members_StudentId",
                table: "StudentsLabs");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Members_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsCourses_Members_StudentId",
                table: "StudentsCourses",
                column: "StudentId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsLabs_Members_StudentId",
                table: "StudentsLabs",
                column: "StudentId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
