using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PencatatanSuhuPekerjaAPI.Migrations
{
    public partial class UpdateTempratureDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                table: "temperature",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "temperature",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
