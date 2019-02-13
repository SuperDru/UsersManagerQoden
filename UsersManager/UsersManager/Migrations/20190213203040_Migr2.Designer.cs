﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UsersManager.Database;

namespace UsersManager.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    [Migration("20190213203040_Migr2")]
    partial class Migr2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("UsersManager.Database.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_departments");

                    b.ToTable("departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "developer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "manager"
                        });
                });

            modelBuilder.Entity("UsersManager.Database.Models.HashedCredential", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id");

                    b.Property<string>("HashedPassword")
                        .HasColumnName("hashed_password");

                    b.Property<byte[]>("Salt")
                        .HasColumnName("salt");

                    b.HasKey("UserId");

                    b.ToTable("hashed_credentials");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            HashedPassword = "s54dyTocSirH9BtCopDzS3sPLvdGsPL6pCpLkjHFhvc=",
                            Salt = new byte[] { 218, 195, 90, 229, 243, 116, 72, 97, 18, 57, 120, 7, 17, 114, 113, 148 }
                        },
                        new
                        {
                            UserId = 2,
                            HashedPassword = "os9LACX+RLCLGp1w2Opv1j8tQMRfKBQdMRXGZT2w4Yw=",
                            Salt = new byte[] { 86, 5, 166, 248, 250, 39, 95, 174, 165, 193, 218, 39, 28, 158, 104, 119 }
                        },
                        new
                        {
                            UserId = 3,
                            HashedPassword = "peL9j7j5WTv8gtB4yTU3TSw70zDE6t/mukXJ2+GjyVw=",
                            Salt = new byte[] { 43, 172, 82, 183, 93, 116, 48, 95, 203, 206, 60, 74, 107, 105, 42, 172 }
                        });
                });

            modelBuilder.Entity("UsersManager.Database.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "manager"
                        },
                        new
                        {
                            Id = 3,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("UsersManager.Database.Models.SalaryRate", b =>
                {
                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.Property<int>("Rate")
                        .HasColumnName("rate");

                    b.HasKey("UpdatedAt", "UserId");

                    b.HasIndex("UserId")
                        .HasName("ix_salary_rates_user_id");

                    b.ToTable("salary_rates");

                    b.HasData(
                        new
                        {
                            UpdatedAt = new DateTime(2014, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1,
                            Rate = 1000
                        },
                        new
                        {
                            UpdatedAt = new DateTime(2016, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1,
                            Rate = 1200
                        },
                        new
                        {
                            UpdatedAt = new DateTime(2015, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 2,
                            Rate = 1500
                        },
                        new
                        {
                            UpdatedAt = new DateTime(2015, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 2,
                            Rate = 1800
                        },
                        new
                        {
                            UpdatedAt = new DateTime(2011, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 3,
                            Rate = 2500
                        });
                });

            modelBuilder.Entity("UsersManager.Database.Models.SalaryRateRequest", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnName("guid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("updated_at");

                    b.Property<string>("Explanation")
                        .HasColumnName("explanation");

                    b.Property<int?>("ManagerId")
                        .HasColumnName("manager_id");

                    b.Property<string>("PrivateExplanation")
                        .HasColumnName("private_explanation");

                    b.Property<int>("Rate")
                        .HasColumnName("rate");

                    b.Property<string>("Reasons")
                        .HasColumnName("reasons");

                    b.Property<int?>("Status")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Guid", "UpdatedAt");

                    b.HasIndex("ManagerId")
                        .HasName("ix_salary_rate_requests_manager_id");

                    b.HasIndex("UserId")
                        .HasName("ix_salary_rate_requests_user_id");

                    b.ToTable("salary_rate_requests");

                    b.HasData(
                        new
                        {
                            Guid = new Guid("744c774f-25eb-4696-8aba-8ec2ca362216"),
                            UpdatedAt = new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Explanation = "You haven't enough experience.",
                            ManagerId = 2,
                            PrivateExplanation = "He's way too dumb.",
                            Rate = 1500,
                            Reasons = "Just so",
                            Status = 3,
                            UserId = 1
                        },
                        new
                        {
                            Guid = new Guid("744c774f-25eb-4696-8aba-8ec2ca362216"),
                            UpdatedAt = new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ManagerId = 2,
                            Rate = 1500,
                            Reasons = "Just so",
                            Status = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("UsersManager.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("DepartmentId")
                        .HasColumnName("department_id");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name");

                    b.Property<Guid>("Guid")
                        .HasColumnName("guid");

                    b.Property<DateTime>("InvitedAt")
                        .HasColumnName("invited_at");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name");

                    b.Property<int?>("ManagerId")
                        .HasColumnName("manager_id");

                    b.Property<string>("NickName")
                        .HasColumnName("nick_name");

                    b.Property<string>("Patronymic")
                        .HasColumnName("patronymic");

                    b.Property<long>("PhoneNumber")
                        .HasColumnName("phone_number");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("DepartmentId")
                        .HasName("ix_users_department_id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ManagerId")
                        .HasName("ix_users_manager_id");

                    b.HasIndex("NickName")
                        .IsUnique();

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentId = 1,
                            Description = "American",
                            Email = "jfoster@gmail.com",
                            FirstName = "Jhon",
                            Guid = new Guid("26a709ce-ce27-43ba-a7d4-6e05ca5368f8"),
                            InvitedAt = new DateTime(2014, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Foster",
                            NickName = "JFoster",
                            PhoneNumber = 89129541254L
                        },
                        new
                        {
                            Id = 2,
                            DepartmentId = 2,
                            Description = "Russian",
                            Email = "ashishkin@mail.ru",
                            FirstName = "Alexander",
                            Guid = new Guid("19b09a1a-3fcf-45ac-88b0-c19b121c52de"),
                            InvitedAt = new DateTime(2015, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Shishkin",
                            NickName = "AShishkin",
                            Patronymic = "Dmitrievich",
                            PhoneNumber = 83149545254L
                        },
                        new
                        {
                            Id = 3,
                            DepartmentId = 2,
                            Description = "Russian",
                            Email = "ashurikov@mail.ru",
                            FirstName = "Andrey",
                            Guid = new Guid("f95bb5bc-1c62-448a-a7ba-2daf05cde42e"),
                            InvitedAt = new DateTime(2011, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Shurikov",
                            NickName = "AShurikov",
                            Patronymic = "Vasilievich",
                            PhoneNumber = 83149565253L
                        });
                });

            modelBuilder.Entity("UsersManager.Database.Models.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId")
                        .HasName("ix_user_roles_user_id");

                    b.ToTable("user_roles");

                    b.HasData(
                        new
                        {
                            RoleId = 3,
                            UserId = 1
                        },
                        new
                        {
                            RoleId = 2,
                            UserId = 2
                        },
                        new
                        {
                            RoleId = 1,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("UsersManager.Database.Models.SalaryRate", b =>
                {
                    b.HasOne("UsersManager.Database.Models.User")
                        .WithMany("SalaryRates")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_salary_rates_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UsersManager.Database.Models.SalaryRateRequest", b =>
                {
                    b.HasOne("UsersManager.Database.Models.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .HasConstraintName("fk_salary_rate_requests_users_manager_id");

                    b.HasOne("UsersManager.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_salary_rate_requests_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UsersManager.Database.Models.User", b =>
                {
                    b.HasOne("UsersManager.Database.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("fk_users_departments_department_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UsersManager.Database.Models.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .HasConstraintName("fk_users_users_manager_id");
                });

            modelBuilder.Entity("UsersManager.Database.Models.UserRole", b =>
                {
                    b.HasOne("UsersManager.Database.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_user_roles_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UsersManager.Database.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_roles_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}