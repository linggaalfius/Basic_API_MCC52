using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fixing_int_NIK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Education_tb_M_University_UniversityId",
                table: "tb_t_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Profiling_tb_t_Education_EducationId",
                table: "tb_t_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_tb_t_Education_UniversityId",
                table: "tb_t_Education");

            migrationBuilder.DropColumn(
                name: "University_id",
                table: "tb_t_Education");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "tb_t_Profiling",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UniversityId",
                table: "tb_t_Education",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId1",
                table: "tb_t_Education",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_Education_UniversityId1",
                table: "tb_t_Education",
                column: "UniversityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Education_tb_M_University_UniversityId1",
                table: "tb_t_Education",
                column: "UniversityId1",
                principalTable: "tb_M_University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Profiling_tb_t_Education_EducationId",
                table: "tb_t_Profiling",
                column: "EducationId",
                principalTable: "tb_t_Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Education_tb_M_University_UniversityId1",
                table: "tb_t_Education");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_Profiling_tb_t_Education_EducationId",
                table: "tb_t_Profiling");

            migrationBuilder.DropIndex(
                name: "IX_tb_t_Education_UniversityId1",
                table: "tb_t_Education");

            migrationBuilder.DropColumn(
                name: "UniversityId1",
                table: "tb_t_Education");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "tb_t_Profiling",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "tb_t_Education",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University_id",
                table: "tb_t_Education",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_Education_UniversityId",
                table: "tb_t_Education",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Education_tb_M_University_UniversityId",
                table: "tb_t_Education",
                column: "UniversityId",
                principalTable: "tb_M_University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_Profiling_tb_t_Education_EducationId",
                table: "tb_t_Profiling",
                column: "EducationId",
                principalTable: "tb_t_Education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
