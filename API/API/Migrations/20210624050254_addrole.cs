using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_t_Role",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_AccountRole",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_AccountRole", x => new { x.NIK, x.RoleID });
                    table.ForeignKey(
                        name: "FK_tb_t_AccountRole_tb_t_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_t_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_t_AccountRole_tb_t_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "tb_t_Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_AccountRole_RoleID",
                table: "tb_t_AccountRole",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_t_AccountRole");

            migrationBuilder.DropTable(
                name: "tb_t_Role");
        }
    }
}
