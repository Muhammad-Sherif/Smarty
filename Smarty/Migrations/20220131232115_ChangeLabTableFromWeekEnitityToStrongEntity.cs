using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class ChangeLabTableFromWeekEnitityToStrongEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsLabs_Labs_LabName_CourseId",
                table: "StudentsLabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsLabs",
                table: "StudentsLabs");

            migrationBuilder.DropIndex(
                name: "IX_StudentsLabs_LabName_CourseId",
                table: "StudentsLabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labs",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "LabName",
                table: "StudentsLabs");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "StudentsLabs",
                newName: "LabId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Labs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsLabs",
                table: "StudentsLabs",
                columns: new[] { "StudentId", "LabId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labs",
                table: "Labs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsLabs_LabId",
                table: "StudentsLabs",
                column: "LabId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsLabs_Labs_LabId",
                table: "StudentsLabs",
                column: "LabId",
                principalTable: "Labs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsLabs_Labs_LabId",
                table: "StudentsLabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsLabs",
                table: "StudentsLabs");

            migrationBuilder.DropIndex(
                name: "IX_StudentsLabs_LabId",
                table: "StudentsLabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labs",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Labs");

            migrationBuilder.RenameColumn(
                name: "LabId",
                table: "StudentsLabs",
                newName: "CourseId");

            migrationBuilder.AddColumn<string>(
                name: "LabName",
                table: "StudentsLabs",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsLabs",
                table: "StudentsLabs",
                columns: new[] { "StudentId", "CourseId", "LabName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labs",
                table: "Labs",
                columns: new[] { "Name", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsLabs_LabName_CourseId",
                table: "StudentsLabs",
                columns: new[] { "LabName", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsLabs_Labs_LabName_CourseId",
                table: "StudentsLabs",
                columns: new[] { "LabName", "CourseId" },
                principalTable: "Labs",
                principalColumns: new[] { "Name", "CourseId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
