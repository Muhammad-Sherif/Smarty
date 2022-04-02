using Microsoft.EntityFrameworkCore.Migrations;
using Smarty.Data.Enums;

#nullable disable

namespace Smarty.Migrations
{
    public partial class SeedingRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    {Guid.NewGuid().ToString() , Roles.Student.ToString(),Roles.Student.ToString().ToUpper() , Guid.NewGuid().ToString() },
                    {Guid.NewGuid().ToString() , Roles.Instructor.ToString(),Roles.Instructor.ToString().ToUpper() , Guid.NewGuid().ToString() },

                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable("AspNetRoles");
        }
    }
}
