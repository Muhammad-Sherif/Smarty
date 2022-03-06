using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class AddManyToManyRelationBetweenStudentAndCourseAttendancesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsAttendances",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsAttendances", x => new { x.StudentId, x.CourseId, x.DateTime });
                    table.ForeignKey(
                        name: "FK_StudentsAttendances_CourseAttendance_DateTime_CourseId",
                        columns: x => new { x.DateTime, x.CourseId },
                        principalTable: "CourseAttendance",
                        principalColumns: new[] { "DateTime", "CourseId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsAttendances_Members_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsAttendances_DateTime_CourseId",
                table: "StudentsAttendances",
                columns: new[] { "DateTime", "CourseId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsAttendances");
        }
    }
}
