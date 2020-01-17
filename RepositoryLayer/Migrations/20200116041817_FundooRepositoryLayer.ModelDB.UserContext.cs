using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundooRepositoryLayer.Migrations
{
  public partial class FundooRepositoryLayerModelDBUserContext : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "UserDetails",
          columns: table => new
          {
            EmployeeId = table.Column<long>(nullable: false)
           .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            FirstName = table.Column<string>(nullable: true),
            LastName = table.Column<string>(nullable: true),
            Email = table.Column<string>(nullable: true),
            Password = table.Column<string>(nullable: true),
            Type = table.Column<string>(nullable: true),
            IsActive = table.Column<bool>(nullable: false),
            IsCreated = table.Column<DateTime>(nullable: false),
            IsModified = table.Column<DateTime>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UserDetails", x => x.EmployeeId);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "UserDetails");
    }
  }
}
