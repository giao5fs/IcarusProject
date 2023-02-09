using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Icarus.Persistence.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "CreatedOnUtc", "Email", "FirstName", "LastModifiedOnUtc", "LastName" },
                values: new object[] { new Guid("783daa08-5f71-407d-82d9-f88e34f5f1ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "harry@gmail.com", "Harry", null, "Porter" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "CreatedOnUtc", "Email", "FirstName", "LastModifiedOnUtc", "LastName" },
                values: new object[] { new Guid("a0bc69ab-6cd9-42b0-849a-767443db8769"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "blair@gmail.com", "Blair", null, "Carager" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
