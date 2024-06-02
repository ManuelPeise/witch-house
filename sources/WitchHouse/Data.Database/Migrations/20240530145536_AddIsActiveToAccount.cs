using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("703125b9-86ca-4049-83cf-94a460e016e8"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AccountTable",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "IsActive", "LastName", "RefreshToken", "Role", "Salt", "Secret", "Token", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("4f9841a9-c894-4075-8aa2-b7e93f4e6d02"), "2024-05-30 16:55", "System", "en", null, null, "", false, "", null, 1, "7df95f2b-c9b6-43c5-818a-eca75ae87ec7", "UEBzc3dvcmQ3ZGY5NWYyYi1jOWI2LTQzYzUtODE4YS1lY2E3NWFlODdlYzc=", null, "", "", "System.Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("4f9841a9-c894-4075-8aa2-b7e93f4e6d02"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AccountTable");

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "LastName", "RefreshToken", "Role", "Salt", "Secret", "Token", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("703125b9-86ca-4049-83cf-94a460e016e8"), "2024-05-29 22:18", "System", "en", null, null, "", "", null, 1, "5e2efb22-abcc-483d-8e14-2a30250f8c35", "UEBzc3dvcmQ1ZTJlZmIyMi1hYmNjLTQ4M2QtOGUxNC0yYTMwMjUwZjhjMzU=", null, "", "", "System.Admin" });
        }
    }
}
