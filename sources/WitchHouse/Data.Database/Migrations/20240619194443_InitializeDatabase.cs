using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("0a785e83-f8d0-4821-959e-e3fe25b3393f"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("686ef574-3961-4340-a56f-3c14a8246bfb"));

            migrationBuilder.DeleteData(
                table: "RolesTable",
                keyColumn: "Id",
                keyValue: new Guid("ea592c9a-ff18-4115-b05e-89c0d3df080c"));

            migrationBuilder.UpdateData(
                table: "CredentialsTable",
                keyColumn: "Id",
                keyValue: new Guid("c8eac614-9788-48b6-aa99-c755bb0d43fa"),
                columns: new[] { "EncodedPassword", "Salt" },
                values: new object[] { "UEBzc3dvcmRkNjVmYjNiMC1mZmQ1LTQzNjQtYTAzZC1mZmM4OTRiMzBkMGI=", new Guid("d65fb3b0-ffd5-4364-a03d-ffc894b30d0b") });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "IsActive", "ModuleName", "ModuleSettingsType", "ModuleType", "SettingsJson", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("54f25e98-f1d1-45e7-80b0-4533f0ba0af0"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-19", "System", true, "SchoolTrainingStatistics", 0, 2, null, "2024-06-19", "System" },
                    { new Guid("f3797d10-8723-44de-bfdc-ec4b49501f03"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-19", "System", true, "SchoolTraining", 0, 1, null, "2024-06-19", "System" }
                });

            migrationBuilder.InsertData(
                table: "RolesTable",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "RoleName", "RoleType", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("e66100cd-6f77-4ee4-b1c6-b9c8dbf205c8"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-19", "System", "Admin", 1, "2024-06-19", "System" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("54f25e98-f1d1-45e7-80b0-4533f0ba0af0"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("f3797d10-8723-44de-bfdc-ec4b49501f03"));

            migrationBuilder.DeleteData(
                table: "RolesTable",
                keyColumn: "Id",
                keyValue: new Guid("e66100cd-6f77-4ee4-b1c6-b9c8dbf205c8"));

            migrationBuilder.UpdateData(
                table: "CredentialsTable",
                keyColumn: "Id",
                keyValue: new Guid("c8eac614-9788-48b6-aa99-c755bb0d43fa"),
                columns: new[] { "EncodedPassword", "Salt" },
                values: new object[] { "UEBzc3dvcmQ5MGU3ZDIyZi02YzcwLTRmNDAtOWI1MC1iZmZlNjYxZWFmZGY=", new Guid("90e7d22f-6c70-4f40-9b50-bffe661eafdf") });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "IsActive", "ModuleName", "ModuleSettingsType", "ModuleType", "SettingsJson", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("0a785e83-f8d0-4821-959e-e3fe25b3393f"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-19", "System", true, "SchoolTrainingStatistics", 0, 2, null, "2024-06-19", "System" },
                    { new Guid("686ef574-3961-4340-a56f-3c14a8246bfb"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-19", "System", true, "SchoolTraining", 0, 1, null, "2024-06-19", "System" }
                });

            migrationBuilder.InsertData(
                table: "RolesTable",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "RoleName", "RoleType", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("ea592c9a-ff18-4115-b05e-89c0d3df080c"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-19", "System", "Admin", 1, "2024-06-19", "System" });
        }
    }
}
