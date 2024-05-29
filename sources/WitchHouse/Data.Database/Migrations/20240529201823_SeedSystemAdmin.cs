using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedSystemAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "LastName", "RefreshToken", "Role", "Salt", "Secret", "Token", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("703125b9-86ca-4049-83cf-94a460e016e8"), "2024-05-29 22:18", "System", "en", null, null, "", "", null, 1, "5e2efb22-abcc-483d-8e14-2a30250f8c35", "UEBzc3dvcmQ1ZTJlZmIyMi1hYmNjLTQ4M2QtOGUxNC0yYTMwMjUwZjhjMzU=", null, "", "", "System.Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("703125b9-86ca-4049-83cf-94a460e016e8"));
        }
    }
}
