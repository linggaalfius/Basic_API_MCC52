using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fix_eduId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education_id",
                table: "tb_t_Profiling");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Education_id",
                table: "tb_t_Profiling",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
