using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CredentialsTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "EncodedPassword", "JwtToken", "MobilePin", "RefreshToken", "Salt", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("c8eac614-9788-48b6-aa99-c755bb0d43fa"), "2024-06-20", "System", "UEBzc3dvcmRmODI1OWQ4Yi1iMDZmLTRjZDktYjQxMS01ODk1M2ZkNTg1MmU=", null, 1234, null, new Guid("f8259d8b-b06f-4cd9-b411-58953fd5852e"), "2024-06-20", "System" });

            migrationBuilder.InsertData(
                table: "DataSyncTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "LastSync", "UpdatedAt", "UpdatedBy", "UserGuid" },
                values: new object[] { new Guid("7029b25c-1748-445f-bb91-dfa97c8147bd"), "2024-06-20", "System", new DateTime(2024, 6, 20, 18, 25, 58, 692, DateTimeKind.Utc).AddTicks(6495), "2024-06-20", "System", new Guid("b5846acb-2911-4831-a9ac-d2342849241d") });

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CredentialGuid", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "IsActive", "LastName", "ProfileImage", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-20", "System", new Guid("c8eac614-9788-48b6-aa99-c755bb0d43fa"), "en", null, null, "", true, "", null, "2024-06-20", "System", "System.Admin" });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "IsActive", "ModuleName", "ModuleSettingsType", "ModuleType", "SettingsJson", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("3c8c305f-8e19-46d2-95e8-f5241587fe77"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-20", "System", true, "SchoolTrainingStatistics", 0, 2, null, "2024-06-20", "System" },
                    { new Guid("b325339a-df39-48e0-bafe-3a6b93ceedff"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-20", "System", true, "SchoolTraining", 0, 1, null, "2024-06-20", "System" }
                });

            migrationBuilder.InsertData(
                table: "RolesTable",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "RoleName", "RoleType", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("12b5ec36-b53d-45ab-b7ae-10fb0e819868"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-20", "System", "Admin", 1, "2024-06-20", "System" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DataSyncTable",
                keyColumn: "Id",
                keyValue: new Guid("7029b25c-1748-445f-bb91-dfa97c8147bd"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("3c8c305f-8e19-46d2-95e8-f5241587fe77"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("b325339a-df39-48e0-bafe-3a6b93ceedff"));

            migrationBuilder.DeleteData(
                table: "RolesTable",
                keyColumn: "Id",
                keyValue: new Guid("12b5ec36-b53d-45ab-b7ae-10fb0e819868"));

            migrationBuilder.DeleteData(
                table: "AccountTable",
                keyColumn: "Id",
                keyValue: new Guid("b5846acb-2911-4831-a9ac-d2342849241d"));

            migrationBuilder.DeleteData(
                table: "CredentialsTable",
                keyColumn: "Id",
                keyValue: new Guid("c8eac614-9788-48b6-aa99-c755bb0d43fa"));
        }
    }
}
