﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PencatatanSuhuPekerjaAPI.Context;

namespace PencatatanSuhuPekerjaAPI.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200915063415_revokedate")]
    partial class revokedate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.Department", b =>
                {
                    b.Property<string>("DepartmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<DateTimeOffset?>("DeletedAt");

                    b.Property<bool?>("IsDelete");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.HasKey("DepartmentId");

                    b.ToTable("tb_m_department");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.Division", b =>
                {
                    b.Property<string>("DivisionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("CreatedAt");

                    b.Property<DateTimeOffset?>("DeletedAt");

                    b.Property<string>("DepartmentId");

                    b.Property<bool?>("IsDelete");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.HasKey("DivisionId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("tb_m_division");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DivisionId");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Salary");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DivisionId");

                    b.ToTable("tb_m_employee");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("tb_m_role");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.Temperature", b =>
                {
                    b.Property<string>("TemperatureId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("EmployeeTemperature");

                    b.HasKey("TemperatureId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("temperature");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("tb_m_user");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("user_role");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.Division", b =>
                {
                    b.HasOne("PencatatanSuhuPekerjaAPI.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.Employee", b =>
                {
                    b.HasOne("PencatatanSuhuPekerjaAPI.Models.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.Temperature", b =>
                {
                    b.HasOne("PencatatanSuhuPekerjaAPI.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.User", b =>
                {
                    b.HasOne("PencatatanSuhuPekerjaAPI.Models.Employee", "Employee")
                        .WithOne("User")
                        .HasForeignKey("PencatatanSuhuPekerjaAPI.Models.User", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PencatatanSuhuPekerjaAPI.Models.UserRole", b =>
                {
                    b.HasOne("PencatatanSuhuPekerjaAPI.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PencatatanSuhuPekerjaAPI.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
