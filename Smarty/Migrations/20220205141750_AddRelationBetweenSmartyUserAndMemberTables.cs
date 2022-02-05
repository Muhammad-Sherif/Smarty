using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smarty.Migrations
{
    public partial class AddRelationBetweenSmartyUserAndMemberTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmartyUserId",
                table: "Members",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Members_SmartyUserId",
                table: "Members",
                column: "SmartyUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AspNetUsers_SmartyUserId",
                table: "Members",
                column: "SmartyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_AspNetUsers_SmartyUserId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_SmartyUserId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SmartyUserId",
                table: "Members");
        }
    }
}
