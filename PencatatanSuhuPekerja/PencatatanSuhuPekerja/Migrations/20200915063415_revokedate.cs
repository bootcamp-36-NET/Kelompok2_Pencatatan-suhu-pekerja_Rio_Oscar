using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PencatatanSuhuPekerjaAPI.Migrations
{
    public partial class revokedate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "tb_m_division");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "tb_m_department");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "tb_m_division",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "tb_m_department",
                newName: "IsDelete");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "tb_m_division",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "tb_m_division",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "tb_m_division",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "tb_m_department",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "tb_m_department",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "tb_m_department",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "tb_m_division");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "tb_m_department");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "tb_m_division",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "tb_m_department",
                newName: "IsDeleted");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "tb_m_division",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "tb_m_division",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "tb_m_division",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "tb_m_department",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "tb_m_department",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "tb_m_department",
                nullable: true);
        }
    }
}
