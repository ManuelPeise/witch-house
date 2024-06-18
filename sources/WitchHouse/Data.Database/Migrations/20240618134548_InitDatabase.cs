using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CredentialsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Salt = table.Column<Guid>(type: "char(36)", nullable: false),
                    MobilePin = table.Column<int>(type: "int", nullable: false),
                    EncodedPassword = table.Column<string>(type: "longtext", nullable: true),
                    JwtToken = table.Column<string>(type: "longtext", nullable: true),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialsTable", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FamilyTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FamilyName = table.Column<string>(type: "longtext", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyTable", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MessageLogTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(type: "longtext", nullable: false),
                    Stacktrace = table.Column<string>(type: "longtext", nullable: false),
                    TimeStamp = table.Column<string>(type: "longtext", nullable: false),
                    Trigger = table.Column<string>(type: "longtext", nullable: false),
                    FamilyGuid = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLogTable", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FamilyGuid = table.Column<Guid>(type: "char(36)", nullable: true),
                    FirstName = table.Column<string>(type: "longtext", nullable: true),
                    LastName = table.Column<string>(type: "longtext", nullable: true),
                    UserName = table.Column<string>(type: "longtext", nullable: false),
                    DateOfBirth = table.Column<string>(type: "longtext", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Culture = table.Column<string>(type: "longtext", nullable: true),
                    ProfileImage = table.Column<string>(type: "longtext", nullable: true),
                    CredentialGuid = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTable_CredentialsTable_CredentialGuid",
                        column: x => x.CredentialGuid,
                        principalTable: "CredentialsTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTable_FamilyTable_FamilyGuid",
                        column: x => x.FamilyGuid,
                        principalTable: "FamilyTable",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ModuleName = table.Column<string>(type: "longtext", nullable: false),
                    ModuleType = table.Column<int>(type: "int", nullable: false),
                    SettingsJson = table.Column<string>(type: "longtext", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccountGuid = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_AccountTable_AccountGuid",
                        column: x => x.AccountGuid,
                        principalTable: "AccountTable",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    RoleType = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "longtext", nullable: true),
                    AccountGuid = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesTable_AccountTable_AccountGuid",
                        column: x => x.AccountGuid,
                        principalTable: "AccountTable",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
                    AccountGuid = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedAt = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitResults_AccountTable_AccountGuid",
                        column: x => x.AccountGuid,
                        principalTable: "AccountTable",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "CredentialsTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "EncodedPassword", "JwtToken", "MobilePin", "RefreshToken", "Salt", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("c8eac614-9788-48b6-aa99-c755bb0d43fa"), "2024-06-18", "System", "UEBzc3dvcmQ3ZDRiMGNhYy1iMjdlLTQzNjYtOGI2Yi1hZDg5YzNjNWZiNmY=", null, 1234, null, new Guid("7d4b0cac-b27e-4366-8b6b-ad89c3c5fb6f"), "2024-06-18", "System" });

            migrationBuilder.InsertData(
                table: "AccountTable",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CredentialGuid", "Culture", "DateOfBirth", "FamilyGuid", "FirstName", "IsActive", "LastName", "ProfileImage", "UpdatedAt", "UpdatedBy", "UserName" },
                values: new object[] { new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-18", "System", new Guid("c8eac614-9788-48b6-aa99-c755bb0d43fa"), "en", null, null, "", true, "", null, "2024-06-18", "System", "System.Admin" });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "IsActive", "ModuleName", "ModuleType", "SettingsJson", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("c601b781-6d81-4f0e-aae1-02838feb753a"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-18", "System", true, "SchoolTraining", 1, null, "2024-06-18", "System" },
                    { new Guid("f034995c-7347-4435-8f22-99d1ffa721b9"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-18", "System", true, "SchoolTrainingStatistics", 2, null, "2024-06-18", "System" }
                });

            migrationBuilder.InsertData(
                table: "RolesTable",
                columns: new[] { "Id", "AccountGuid", "CreatedAt", "CreatedBy", "RoleName", "RoleType", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("2f0d2e15-ec36-4dac-a18a-57aad4504312"), new Guid("b5846acb-2911-4831-a9ac-d2342849241d"), "2024-06-18", "System", "Admin", 1, "2024-06-18", "System" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTable_CredentialGuid",
                table: "AccountTable",
                column: "CredentialGuid");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTable_FamilyGuid",
                table: "AccountTable",
                column: "FamilyGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_AccountGuid",
                table: "Modules",
                column: "AccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_RolesTable_AccountGuid",
                table: "RolesTable",
                column: "AccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_UnitResults_AccountGuid",
                table: "UnitResults",
                column: "AccountGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageLogTable");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "RolesTable");

            migrationBuilder.DropTable(
                name: "UnitResults");

            migrationBuilder.DropTable(
                name: "AccountTable");

            migrationBuilder.DropTable(
                name: "CredentialsTable");

            migrationBuilder.DropTable(
                name: "FamilyTable");
        }
    }
}
