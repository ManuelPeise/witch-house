using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("8322a4c8-b498-415c-8021-61d40c47e020"));

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "IsActive", "LastName", "Pin", "RefreshToken", "Role", "Salt", "Secret", "Token", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("2e65c828-397f-4d75-af20-4d94d7bbdb45"), "2024-06-05 22:03", "System", "en", null, null, "", false, "", "", null, 1, "bacb3fe6-efe9-46e7-801b-bb2b4b9f6028", "UEBzc3dvcmRiYWNiM2ZlNi1lZmU5LTQ2ZTctODAxYi1iYjJiNGI5ZjYwMjg=", null, "", "", "System.Admin" });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("c62daaff-4f9b-4283-a1ea-1a9f42df2d99"),
                column: "ModuleType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cd472d2e-28c9-42b0-9cba-536b0ddb923b"),
                column: "ModuleType",
                value: 2);

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "ModuleName", "ModuleType", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("6f9aed6f-07cf-4dcf-8b9f-e5fd52b35ffc"), "2024.06.05", "System", "MobileApp", 0, "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("2e65c828-397f-4d75-af20-4d94d7bbdb45"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("6f9aed6f-07cf-4dcf-8b9f-e5fd52b35ffc"));

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "IsActive", "LastName", "Pin", "RefreshToken", "Role", "Salt", "Secret", "Token", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("8322a4c8-b498-415c-8021-61d40c47e020"), "2024-06-05 21:46", "System", "en", null, null, "", false, "", "", null, 1, "bff72b84-2bf5-4fc2-9c2f-2b21536f6d33", "UEBzc3dvcmRiZmY3MmI4NC0yYmY1LTRmYzItOWMyZi0yYjIxNTM2ZjZkMzM=", null, "", "", "System.Admin" });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("c62daaff-4f9b-4283-a1ea-1a9f42df2d99"),
                column: "ModuleType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cd472d2e-28c9-42b0-9cba-536b0ddb923b"),
                column: "ModuleType",
                value: 1);
        }
    }
}
