using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessPensionModule.Migrations
{
    public partial class AddPensionDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PensionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AadharNumber = table.Column<long>(type: "bigint", nullable: false),
                    SalaryEarned = table.Column<long>(type: "bigint", nullable: false),
                    Allowances = table.Column<long>(type: "bigint", nullable: false),
                    PensionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", nullable: false),
                    PensionAmount = table.Column<double>(type: "float", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankServiceCharge = table.Column<double>(type: "float", nullable: false),
                    TransectionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PensionDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PensionDetails");
        }
    }
}
