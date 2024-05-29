using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class ExtendLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FamilyGuid",
                table: "MessageLogTable",
                type: "char(36)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyGuid",
                table: "MessageLogTable");
        }
    }
}
