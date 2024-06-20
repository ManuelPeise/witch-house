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

                    b.Property<Guid?>("CredentialGuid")
                        .HasColumnType("char(36)");

                    b.Property<string>("Culture")
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

                    b.Property<string>("ProfileImage")
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

                    b.HasIndex("CredentialGuid");

                    b.HasIndex("FamilyGuid");

                    b.ToTable("AccountTable");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b5846acb-2911-4831-a9ac-d2342849241d"),
                            CreatedAt = "2024-06-20",
                            CreatedBy = "System",
                            CredentialGuid = new Guid("c8eac614-9788-48b6-aa99-c755bb0d43fa"),
                            Culture = "en",
                            FirstName = "",
                            IsActive = true,
                            LastName = "",
                            UpdatedAt = "2024-06-20",
                            UpdatedBy = "System",
                            UserName = "System.Admin"
                        });
                });

            modelBuilder.Entity("Data.Shared.Entities.CredentialEntity", b =>
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

                    b.Property<string>("EncodedPassword")
                        .HasColumnType("longtext");

                    b.Property<string>("JwtToken")
                        .HasColumnType("longtext");

                    b.Property<int>("MobilePin")
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<Guid>("Salt")
                        .HasColumnType("char(36)");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CredentialsTable");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c8eac614-9788-48b6-aa99-c755bb0d43fa"),
                            CreatedAt = "2024-06-20",
                            CreatedBy = "System",
                            EncodedPassword = "UEBzc3dvcmRmODI1OWQ4Yi1iMDZmLTRjZDktYjQxMS01ODk1M2ZkNTg1MmU=",
                            MobilePin = 1234,
                            Salt = new Guid("f8259d8b-b06f-4cd9-b411-58953fd5852e"),
                            UpdatedAt = "2024-06-20",
                            UpdatedBy = "System"
                        });
                });

            modelBuilder.Entity("Data.Shared.Entities.DataSyncEntity", b =>
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

                    b.Property<DateTime>("LastSync")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("DataSyncTable");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7029b25c-1748-445f-bb91-dfa97c8147bd"),
                            CreatedAt = "2024-06-20",
                            CreatedBy = "System",
                            LastSync = new DateTime(2024, 6, 20, 18, 25, 58, 692, DateTimeKind.Utc).AddTicks(6495),
                            UpdatedAt = "2024-06-20",
                            UpdatedBy = "System",
                            UserGuid = new Guid("b5846acb-2911-4831-a9ac-d2342849241d")
                        });
                });

            modelBuilder.Entity("Data.Shared.Entities.FamilyEntity", b =>
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

                    b.Property<string>("FamilyFullName")
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

                    b.Property<Guid?>("AccountGuid")
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ModuleSettingsType")
                        .HasColumnType("int");

                    b.Property<int>("ModuleType")
                        .HasColumnType("int");

                    b.Property<string>("SettingsJson")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountGuid");

                    b.ToTable("Modules");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b325339a-df39-48e0-bafe-3a6b93ceedff"),
                            AccountGuid = new Guid("b5846acb-2911-4831-a9ac-d2342849241d"),
                            CreatedAt = "2024-06-20",
                            CreatedBy = "System",
                            IsActive = true,
                            ModuleName = "SchoolTraining",
                            ModuleSettingsType = 0,
                            ModuleType = 1,
                            UpdatedAt = "2024-06-20",
                            UpdatedBy = "System"
                        },
                        new
                        {
                            Id = new Guid("3c8c305f-8e19-46d2-95e8-f5241587fe77"),
                            AccountGuid = new Guid("b5846acb-2911-4831-a9ac-d2342849241d"),
                            CreatedAt = "2024-06-20",
                            CreatedBy = "System",
                            IsActive = true,
                            ModuleName = "SchoolTrainingStatistics",
                            ModuleSettingsType = 0,
                            ModuleType = 2,
                            UpdatedAt = "2024-06-20",
                            UpdatedBy = "System"
                        });
                });

            modelBuilder.Entity("Data.Shared.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AccountGuid")
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleType")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountGuid");

                    b.ToTable("RolesTable");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12b5ec36-b53d-45ab-b7ae-10fb0e819868"),
                            AccountGuid = new Guid("b5846acb-2911-4831-a9ac-d2342849241d"),
                            CreatedAt = "2024-06-20",
                            CreatedBy = "System",
                            RoleName = "Admin",
                            RoleType = 1,
                            UpdatedAt = "2024-06-20",
                            UpdatedBy = "System"
                        });
                });

            modelBuilder.Entity("Data.Shared.Entities.UnitResultEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AccountGuid")
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

                    b.HasIndex("AccountGuid");

                    b.ToTable("UnitResults");
                });

            modelBuilder.Entity("Data.Shared.Entities.AccountEntity", b =>
                {
                    b.HasOne("Data.Shared.Entities.CredentialEntity", "Credentials")
                        .WithMany()
                        .HasForeignKey("CredentialGuid");

                    b.HasOne("Data.Shared.Entities.FamilyEntity", "Family")
                        .WithMany("Accounts")
                        .HasForeignKey("FamilyGuid");

                    b.Navigation("Credentials");

                    b.Navigation("Family");
                });

            modelBuilder.Entity("Data.Shared.Entities.ModuleEntity", b =>
                {
                    b.HasOne("Data.Shared.Entities.AccountEntity", "Account")
                        .WithMany("Modules")
                        .HasForeignKey("AccountGuid");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Data.Shared.Entities.RoleEntity", b =>
                {
                    b.HasOne("Data.Shared.Entities.AccountEntity", "Account")
                        .WithMany("UserRoles")
                        .HasForeignKey("AccountGuid");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Data.Shared.Entities.UnitResultEntity", b =>
                {
                    b.HasOne("Data.Shared.Entities.AccountEntity", "Account")
                        .WithMany("UnitResults")
                        .HasForeignKey("AccountGuid");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Data.Shared.Entities.AccountEntity", b =>
                {
                    b.Navigation("Modules");

                    b.Navigation("UnitResults");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Data.Shared.Entities.FamilyEntity", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
