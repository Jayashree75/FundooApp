using Microsoft.EntityFrameworkCore.Migrations;

namespace FundooRepositoryLayer.Migrations
{
    public partial class changedIdColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "UserDetails",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_Email",
                table: "UserDetails",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDetails_Email",
                table: "UserDetails");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserDetails",
                newName: "EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
