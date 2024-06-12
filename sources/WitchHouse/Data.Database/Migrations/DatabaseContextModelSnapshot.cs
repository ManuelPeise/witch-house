﻿// <auto-generated />
using System;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Data.Shared.Entities.AccountEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("FamilyGuid")
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AccountTable");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dac4b8f9-4ece-4bed-95f9-860d4d53924f"),
                            CreatedAt = "2024-06-12 21:17",
                            CreatedBy = "System",
                            Culture = "en",
                            FirstName = "",
                            IsActive = false,
                            LastName = "",
                            Pin = "",
                            Role = 1,
                            Salt = "f760b21a-e1b1-4929-9e9a-55e705408202",
                            Secret = "UEBzc3dvcmRmNzYwYjIxYS1lMWIxLTQ5MjktOWU5YS01NWU3MDU0MDgyMDI=",
                            UpdatedAt = "",
                            UpdatedBy = "",
                            UserName = "System.Admin"
                        });
                });

            modelBuilder.Entity("Data.Shared.Entities.FamilyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FamilyTable");
                });

            modelBuilder.Entity("Data.Shared.Entities.LogMessageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("FamilyGuid")
                        .HasColumnType("char(36)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Stacktrace")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TimeStamp")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Trigger")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("MessageLogTable");
                });

            modelBuilder.Entity("Data.Shared.Entities.ModuleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ModuleType")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Modules");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6f9aed6f-07cf-4dcf-8b9f-e5fd52b35ffc"),
                            CreatedAt = "2024.06.12",
                            CreatedBy = "System",
                            ModuleName = "MobileApp",
                            ModuleType = 0,
                            UpdatedAt = "",
                            UpdatedBy = ""
                        },
                        new
                        {
                            Id = new Guid("c62daaff-4f9b-4283-a1ea-1a9f42df2d99"),
                            CreatedAt = "2024.06.12",
                            CreatedBy = "System",
                            ModuleName = "SchoolTraining",
                            ModuleType = 1,
                            UpdatedAt = "",
                            UpdatedBy = ""
                        },
                        new
                        {
                            Id = new Guid("cd472d2e-28c9-42b0-9cba-536b0ddb923b"),
                            CreatedAt = "2024.06.12",
                            CreatedBy = "System",
                            ModuleName = "SchoolTrainingStatistics",
                            ModuleType = 2,
                            UpdatedAt = "",
                            UpdatedBy = ""
                        });
                });

            modelBuilder.Entity("Data.Shared.Entities.SettingsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ModuleType")
                        .HasColumnType("int");

                    b.Property<string>("SettingsJson")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SettingsType")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Data.Shared.Entities.UnitResultEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Failed")
                        .HasColumnType("int");

                    b.Property<int>("Success")
                        .HasColumnType("int");

                    b.Property<string>("TimeStamp")
                        .HasColumnType("longtext");

                    b.Property<int>("UnitType")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("UnitResults");
                });

            modelBuilder.Entity("Data.Shared.Entities.UserModuleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("char(36)");

                    b.Property<int>("ModuleType")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("UserModules");
                });
#pragma warning restore 612, 618
        }
    }
}
