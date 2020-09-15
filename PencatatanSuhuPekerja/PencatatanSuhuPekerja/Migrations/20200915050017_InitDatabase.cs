using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PencatatanSuhuPekerjaAPI.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_department",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_division",
                columns: table => new
                {
                    DivisionId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DepartmentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_division", x => x.DivisionId);
                    table.ForeignKey(
                        name: "FK_tb_m_division_tb_m_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "tb_m_department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Salary = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    DivisionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_division_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "tb_m_division",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_user",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_user_tb_m_employee_Id",
                        column: x => x.Id,
                        principalTable: "tb_m_employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "temperature",
                columns: table => new
                {
                    TemperatureId = table.Column<string>(nullable: false),
                    EmployeeTemperature = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_temperature", x => x.TemperatureId);
                    table.ForeignKey(
                        name: "FK_temperature_tb_m_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_m_employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_user_role_tb_m_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_tb_m_user_UserId",
                        column: x => x.UserId,
                        principalTable: "tb_m_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_division_DepartmentId",
                table: "tb_m_division",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_DivisionId",
                table: "tb_m_employee",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_temperature_EmployeeId",
                table: "temperature",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_RoleId",
                table: "user_role",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "temperature");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "tb_m_role");

            migrationBuilder.DropTable(
                name: "tb_m_user");

            migrationBuilder.DropTable(
                name: "tb_m_employee");

            migrationBuilder.DropTable(
                name: "tb_m_division");

            migrationBuilder.DropTable(
                name: "tb_m_department");
        }
    }
}
