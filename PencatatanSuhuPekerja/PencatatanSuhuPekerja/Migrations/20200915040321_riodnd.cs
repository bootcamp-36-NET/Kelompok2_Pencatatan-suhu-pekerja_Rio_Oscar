using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PencatatanSuhuPekerjaAPI.Migrations
{
    public partial class riodnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_departments",
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
                    table.PrimaryKey("PK_tb_m_departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_divisions",
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
                    table.PrimaryKey("PK_tb_m_divisions", x => x.DivisionId);
                    table.ForeignKey(
                        name: "FK_tb_m_divisions_tb_m_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "tb_m_departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_divisions_DepartmentId",
                table: "tb_m_divisions",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_divisions");

            migrationBuilder.DropTable(
                name: "tb_m_departments");
        }
    }
}
