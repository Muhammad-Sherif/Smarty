using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class AddAcceptedScanDistancePropertyToCourseAttendanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AcceptedScanDistance",
                table: "CourseAttendance",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedScanDistance",
                table: "CourseAttendance");
        }
    }
}
