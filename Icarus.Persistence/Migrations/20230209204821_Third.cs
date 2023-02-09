using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Icarus.Persistence.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("783daa08-5f71-407d-82d9-f88e34f5f1ad"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("a0bc69ab-6cd9-42b0-849a-767443db8769"));

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "CreatedOnUtc", "Email", "FirstName", "LastModifiedOnUtc", "LastName" },
                values: new object[] { new Guid("0e31524a-e58f-444a-8d5a-c1145313bbea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blair", "Carager", null, "blair@gmail.com" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "CreatedOnUtc", "Email", "FirstName", "LastModifiedOnUtc", "LastName" },
                values: new object[] { new Guid("c88c5fe4-d252-455b-806d-c1b311dedd50"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry", "Porter", null, "harry@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("0e31524a-e58f-444a-8d5a-c1145313bbea"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("c88c5fe4-d252-455b-806d-c1b311dedd50"));

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "CreatedOnUtc", "Email", "FirstName", "LastModifiedOnUtc", "LastName" },
                values: new object[] { new Guid("783daa08-5f71-407d-82d9-f88e34f5f1ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "harry@gmail.com", "Harry", null, "Porter" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "CreatedOnUtc", "Email", "FirstName", "LastModifiedOnUtc", "LastName" },
                values: new object[] { new Guid("a0bc69ab-6cd9-42b0-849a-767443db8769"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "blair@gmail.com", "Blair", null, "Carager" });
        }
    }
}
