using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSyncData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "UEBzc3dvcmQxMDFlOWFiNS0xZDk2LTQ3ZjctODc5ZS1hNmRkZWM3MWIyMTQ=", new Guid("101e9ab5-1d96-47f7-879e-a6ddec71b214") });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "IsActive", "ModuleName", "ModuleSettingsType", "ModuleType", "SettingsJson", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("4660a0c8-552a-43ae-bf61-597fef1b79b1"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-19", "System", true, "SchoolTraining", 0, 1, null, "2024-06-19", "System" },
                    { new Guid("6318fc80-7a9d-458e-a279-2e8e317c3420"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-19", "System", true, "SchoolTrainingStatistics", 0, 2, null, "2024-06-19", "System" }
                });

            migrationBuilder.InsertData(
                table: "RolesTable",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "RoleName", "RoleType", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("088eca49-858e-466f-8e32-75b4ed89e98c"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-19", "System", "Admin", 1, "2024-06-19", "System" });

            var sql = $"INSERT INTO datasynctable (Id,UserGuid,LastSync,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy)" +
                      $"SELECT '{Guid.NewGuid().ToString()}',Id,'{DateTime.Now.ToString("yyyy-MM-dd")}','{DateTime.Now.ToString("yyyy-MM-dd")}','System','{DateTime.Now.ToString("yyyy-MM-dd")}','System' FROM accounttable;";
      

            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("4660a0c8-552a-43ae-bf61-597fef1b79b1"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("6318fc80-7a9d-458e-a279-2e8e317c3420"));

            migrationBuilder.DeleteData(
                table: "RolesTable",
                keyColumn: "Id",
                keyValue: new Guid("088eca49-858e-466f-8e32-75b4ed89e98c"));

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
    }
}
