using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class ChangeNameAndTypeOfYearPropertyInStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // commented because it applying to database before but it migration not exist 
            //migrationBuilder.DropColumn(
            //    name: "Year",
            //    table: "Members");

            //migrationBuilder.AddColumn<string>(
            //    name: "UniversityYear",
            //    table: "Members",
            //    type: "nvarchar(250)",
            //    maxLength: 250,
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "UniversityYear",
            //    table: "Members");

            //migrationBuilder.AddColumn<int>(
            //    name: "Year",
            //    table: "Members",
            //    type: "int",
            //    nullable: true);
        }
    }
}
