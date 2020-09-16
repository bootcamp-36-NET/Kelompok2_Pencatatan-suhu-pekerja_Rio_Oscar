using Microsoft.EntityFrameworkCore.Migrations;

namespace PencatatanSuhuPekerjaAPI.Migrations
{
    public partial class revokeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "tb_m_division",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "tb_m_department",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_m_division",
                newName: "DivisionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_m_department",
                newName: "DepartmentId");
        }
    }
}
