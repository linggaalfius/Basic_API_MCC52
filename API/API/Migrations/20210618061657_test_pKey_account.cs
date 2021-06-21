using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class test_pKey_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_M_University",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_M_University", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_Account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_Account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_t_Account_tb_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_Education",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    University_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_Education", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_tb_t_Education_tb_M_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "tb_M_University",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_Profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Education_id = table.Column<int>(type: "int", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_Profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_t_Profiling_tb_t_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_t_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_t_Profiling_tb_t_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "tb_t_Education",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_Education_UniversityId",
                table: "tb_t_Education",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_Profiling_EducationId",
                table: "tb_t_Profiling",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_t_Profiling");

            migrationBuilder.DropTable(
                name: "tb_t_Account");

            migrationBuilder.DropTable(
                name: "tb_t_Education");

            migrationBuilder.DropTable(
                name: "tb_M_University");
        }
    }
}
