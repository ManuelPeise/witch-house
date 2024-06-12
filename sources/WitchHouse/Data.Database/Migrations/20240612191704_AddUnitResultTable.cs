using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitResultTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("2e65c828-397f-4d75-af20-4d94d7bbdb45"));

            migrationBuilder.CreateTable(
                name: "UnitResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Success = table.Column<int>(type: "int", nullable: false),
                    Failed = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<string>(type: "longtext", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitResults", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "IsActive", "LastName", "Pin", "RefreshToken", "Role", "Salt", "Secret", "Token", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("dac4b8f9-4ece-4bed-95f9-860d4d53924f"), "2024-06-12 21:17", "System", "en", null, null, "", false, "", "", null, 1, "f760b21a-e1b1-4929-9e9a-55e705408202", "UEBzc3dvcmRmNzYwYjIxYS1lMWIxLTQ5MjktOWU5YS01NWU3MDU0MDgyMDI=", null, "", "", "System.Admin" });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("6f9aed6f-07cf-4dcf-8b9f-e5fd52b35ffc"),
                column: "CreatedAt",
                value: "2024.06.12");

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("c62daaff-4f9b-4283-a1ea-1a9f42df2d99"),
                column: "CreatedAt",
                value: "2024.06.12");

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cd472d2e-28c9-42b0-9cba-536b0ddb923b"),
                column: "CreatedAt",
                value: "2024.06.12");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitResults");

            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("dac4b8f9-4ece-4bed-95f9-860d4d53924f"));

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "IsActive", "LastName", "Pin", "RefreshToken", "Role", "Salt", "Secret", "Token", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("2e65c828-397f-4d75-af20-4d94d7bbdb45"), "2024-06-05 22:03", "System", "en", null, null, "", false, "", "", null, 1, "bacb3fe6-efe9-46e7-801b-bb2b4b9f6028", "UEBzc3dvcmRiYWNiM2ZlNi1lZmU5LTQ2ZTctODAxYi1iYjJiNGI5ZjYwMjg=", null, "", "", "System.Admin" });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("6f9aed6f-07cf-4dcf-8b9f-e5fd52b35ffc"),
                column: "CreatedAt",
                value: "2024.06.05");

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
    }
}
