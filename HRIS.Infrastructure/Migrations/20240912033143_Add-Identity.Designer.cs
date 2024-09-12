﻿// <auto-generated />
using System;
using HRIS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRIS.Infrastructure.Migrations
{
    [DbContext(typeof(HRISContext))]
    [Migration("20240912033143_Add-Identity")]
    partial class AddIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HRIS.Domain.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Department", b =>
                {
                    b.Property<int>("Deptno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Deptno"));

                    b.Property<string>("Deptname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("deptname");

                    b.Property<string>("Location")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("location");

                    b.Property<int?>("Mgrempno")
                        .HasColumnType("integer")
                        .HasColumnName("mgrempno");

                    b.Property<int?>("Spvempno")
                        .HasColumnType("integer")
                        .HasColumnName("spvempno");

                    b.HasKey("Deptno")
                        .HasName("departments_pkey");

                    b.HasIndex("Location");

                    b.HasIndex("Mgrempno");

                    b.HasIndex("Spvempno");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Dependent", b =>
                {
                    b.Property<int>("Dependentno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("dependentno");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Dependentno"));

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<int?>("Empno")
                        .HasColumnType("integer")
                        .HasColumnName("empno");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("relationship");

                    b.Property<string>("Sex")
                        .HasColumnType("character varying")
                        .HasColumnName("sex");

                    b.HasKey("Dependentno")
                        .HasName("dependents_pkey");

                    b.HasIndex("Empno");

                    b.ToTable("Dependents");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Empno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("empno");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Empno"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("address");

                    b.Property<int?>("Deptno")
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("Employeetype")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("employeetype");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("fname");

                    b.Property<DateTime?>("Lastupdateddate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("lastupdateddate")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int?>("Level")
                        .HasColumnType("integer")
                        .HasColumnName("level");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("lname");

                    b.Property<int?>("Nik")
                        .HasColumnType("integer")
                        .HasColumnName("nik");

                    b.Property<int?>("Phonenumber")
                        .HasColumnType("integer")
                        .HasColumnName("phonenumber");

                    b.Property<string>("Position")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("position");

                    b.Property<int?>("Salary")
                        .HasColumnType("integer")
                        .HasColumnName("salary");

                    b.Property<string>("Sex")
                        .HasColumnType("character varying")
                        .HasColumnName("sex");

                    b.Property<string>("Status")
                        .HasColumnType("character varying")
                        .HasColumnName("status");

                    b.Property<string>("Statusreason")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("statusreason");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Empno")
                        .HasName("employees_pkey");

                    b.HasIndex("Deptno");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Location", b =>
                {
                    b.Property<string>("Locations")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("location");

                    b.HasKey("Locations")
                        .HasName("location_pkey");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Projno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("projno");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Projno"));

                    b.Property<int?>("Deptno")
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    b.Property<string>("Projectlocation")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("projectlocation");

                    b.Property<string>("Projname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("projname");

                    b.HasKey("Projno")
                        .HasName("projects_pkey");

                    b.HasIndex("Deptno");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Workson", b =>
                {
                    b.Property<int>("Empno")
                        .HasColumnType("integer")
                        .HasColumnName("empno");

                    b.Property<int>("Projno")
                        .HasColumnType("integer")
                        .HasColumnName("projno");

                    b.Property<DateOnly>("Dateworked")
                        .HasColumnType("date")
                        .HasColumnName("dateworked");

                    b.Property<int?>("Hoursworked")
                        .HasColumnType("integer")
                        .HasColumnName("hoursworked");

                    b.HasKey("Empno", "Projno")
                        .HasName("workson_pkey");

                    b.HasIndex("Projno");

                    b.ToTable("Worksons");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Department", b =>
                {
                    b.HasOne("HRIS.Domain.Entities.Location", "LocationNavigation")
                        .WithMany("Departments")
                        .HasForeignKey("Location")
                        .HasConstraintName("fk_location");

                    b.HasOne("HRIS.Domain.Entities.Employee", "MgrempnoNavigation")
                        .WithMany("DepartmentMgrempnoNavigations")
                        .HasForeignKey("Mgrempno")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("departments_mgrempno_fkey");

                    b.HasOne("HRIS.Domain.Entities.Employee", "SpvempnoNavigation")
                        .WithMany("DepartmentSpvempnoNavigations")
                        .HasForeignKey("Spvempno")
                        .HasConstraintName("fk_spvempno");

                    b.Navigation("LocationNavigation");

                    b.Navigation("MgrempnoNavigation");

                    b.Navigation("SpvempnoNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Dependent", b =>
                {
                    b.HasOne("HRIS.Domain.Entities.Employee", "EmpnoNavigation")
                        .WithMany("Dependents")
                        .HasForeignKey("Empno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("dependents_empno_fkey");

                    b.Navigation("EmpnoNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Employee", b =>
                {
                    b.HasOne("HRIS.Domain.Entities.Department", "DeptnoNavigation")
                        .WithMany("Employees")
                        .HasForeignKey("Deptno")
                        .HasConstraintName("fk_deptno");

                    b.HasOne("HRIS.Domain.Entities.AppUser", "AppUser")
                        .WithMany("Employees")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");

                    b.Navigation("DeptnoNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Project", b =>
                {
                    b.HasOne("HRIS.Domain.Entities.Department", "DeptnoNavigation")
                        .WithMany("Projects")
                        .HasForeignKey("Deptno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("projects_deptno_fkey");

                    b.Navigation("DeptnoNavigation");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Workson", b =>
                {
                    b.HasOne("HRIS.Domain.Entities.Employee", "EmpnoNavigation")
                        .WithMany("Worksons")
                        .HasForeignKey("Empno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("workson_empno_fkey");

                    b.HasOne("HRIS.Domain.Entities.Project", "ProjnoNavigation")
                        .WithMany("Worksons")
                        .HasForeignKey("Projno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("workson_projno_fkey");

                    b.Navigation("EmpnoNavigation");

                    b.Navigation("ProjnoNavigation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HRIS.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HRIS.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRIS.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HRIS.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HRIS.Domain.Entities.AppUser", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Employee", b =>
                {
                    b.Navigation("DepartmentMgrempnoNavigations");

                    b.Navigation("DepartmentSpvempnoNavigations");

                    b.Navigation("Dependents");

                    b.Navigation("Worksons");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Location", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("HRIS.Domain.Entities.Project", b =>
                {
                    b.Navigation("Worksons");
                });
#pragma warning restore 612, 618
        }
    }
}
