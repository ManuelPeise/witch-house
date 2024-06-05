using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class ExtendAccountEntityWithPin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("9ea3e392-07d7-4e58-b177-0d684ee41992"));

            migrationBuilder.AddColumn<string>(
                name: "Pin",
                table: "AccountTable",
                type: "longtext",
                nullable: false);

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "IsActive", "LastName", "Pin", "RefreshToken", "Role", "Salt", "Secret", "Token", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("8322a4c8-b498-415c-8021-61d40c47e020"), "2024-06-05 21:46", "System", "en", null, null, "", false, "", "", null, 1, "bff72b84-2bf5-4fc2-9c2f-2b21536f6d33", "UEBzc3dvcmRiZmY3MmI4NC0yYmY1LTRmYzItOWMyZi0yYjIxNTM2ZjZkMzM=", null, "", "", "System.Admin" });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("c62daaff-4f9b-4283-a1ea-1a9f42df2d99"),
                column: "CreatedAt",
                value: "2024.06.05");

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cd472d2e-28c9-42b0-9cba-536b0ddb923b"),
                column: "CreatedAt",
                value: "2024.06.05");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("8322a4c8-b498-415c-8021-61d40c47e020"));

            migrationBuilder.DropColumn(
                name: "Pin",
                table: "AccountTable");

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "IsActive", "LastName", "RefreshToken", "Role", "Salt", "Secret", "Token", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("9ea3e392-07d7-4e58-b177-0d684ee41992"), "2024-06-04 15:59", "System", "en", null, null, "", false, "", null, 1, "1a104d76-7e0b-4765-a2f3-9389d88b3ff8", "UEBzc3dvcmQxYTEwNGQ3Ni03ZTBiLTQ3NjUtYTJmMy05Mzg5ZDg4YjNmZjg=", null, "", "", "System.Admin" });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("c62daaff-4f9b-4283-a1ea-1a9f42df2d99"),
                column: "CreatedAt",
                value: "2024.06.04");

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cd472d2e-28c9-42b0-9cba-536b0ddb923b"),
                column: "CreatedAt",
                value: "2024.06.04");
        }
    }
}
