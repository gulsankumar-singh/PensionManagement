using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PensionerDetailModule.Migrations
{
    public partial class addPensionerAndBankTblToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AadharNumber = table.Column<long>(type: "bigint", nullable: false),
                    SalaryEarned = table.Column<long>(type: "bigint", nullable: false),
                    Allowances = table.Column<long>(type: "bigint", nullable: false),
                    PensionType = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankDetail_BankDetails_BankId",
                        column: x => x.BankId,
                        principalTable: "BankDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BankDetails",
                columns: new[] { "Id", "BankName", "BankType" },
                values: new object[,]
                {
                    { 1, "HDFC Bank Ltd.", 1 },
                    { 2, "State Bank of India", 0 },
                    { 3, "ICICI Bank Ltd.", 1 },
                    { 4, "Kotak Mahindra Bank", 1 },
                    { 5, "Axis Bank Ltd.", 1 },
                    { 6, "Indusland Bank Ltd.", 1 },
                    { 7, "Yes Bank Ltd.", 1 },
                    { 8, "Panjab National Bank", 0 },
                    { 9, "Bank of Baroda", 0 },
                    { 10, "Bank of India", 0 }
                });

            migrationBuilder.InsertData(
                table: "BankDetail",
                columns: new[] { "Id", "AadharNumber", "AccountNumber", "Allowances", "BankId", "DateOfBirth", "Name", "PAN", "PensionType", "SalaryEarned" },
                values: new object[,]
                {
                    { 2, 854728326906L, 81730197274L, 26704L, 1, new DateTime(1991, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sourav", "KDRTI7666A", 0, 468418L },
                    { 16, 471714613539L, 204687301318L, 37704L, 9, new DateTime(1996, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gulshan", "ZEOSA8100V", 1, 869460L },
                    { 3, 974455078114L, 40740232906L, 36476L, 9, new DateTime(1974, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sameer", "ZWKOF2689R", 1, 865912L },
                    { 19, 280225974218L, 349195618403L, 28821L, 8, new DateTime(1999, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Akash", "YEBSQ6580R", 0, 629579L },
                    { 10, 337877837167L, 639415907726L, 14845L, 8, new DateTime(1995, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anjali", "VOVUQ4952B", 1, 593084L },
                    { 5, 389444809498L, 560710767357L, 1406L, 7, new DateTime(2002, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saket", "UOYCV0783O", 0, 885577L },
                    { 15, 960272640642L, 162572811085L, 14068L, 6, new DateTime(1976, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aditya", "UNVHI8166X", 0, 710003L },
                    { 13, 869852758857L, 571881193550L, 3807L, 5, new DateTime(1984, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sunidhi", "OYGXW4443E", 0, 866978L },
                    { 4, 461755449180L, 467028762867L, 31476L, 5, new DateTime(1980, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vivaan", "IXSUT5186S", 0, 925642L },
                    { 1, 791714947214L, 968557297810L, 19292L, 5, new DateTime(2000, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ajay", "KSMOC9374L", 0, 436986L },
                    { 8, 874313859213L, 779371944069L, 29185L, 4, new DateTime(1987, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pragya", "GOWUN8949C", 0, 461227L },
                    { 6, 937162602552L, 315827677945L, 38035L, 4, new DateTime(1991, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rahul", "GPPVI6213L", 1, 619564L },
                    { 14, 150242523686L, 657059136087L, 41707L, 3, new DateTime(1963, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rohit", "YIUGX7931I", 0, 810046L },
                    { 7, 287244420393L, 2473327567L, 45100L, 3, new DateTime(1957, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rohan", "BXLEY3454O", 1, 593845L },
                    { 18, 930139439179L, 713766531809L, 4976L, 2, new DateTime(1975, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ujjwal", "QAQBL8395S", 0, 575598L },
                    { 11, 888861335475L, 297580783520L, 39151L, 2, new DateTime(1994, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ranjeet", "BIOCM9234K", 1, 752833L },
                    { 17, 653515346579L, 23368227618L, 24871L, 1, new DateTime(1978, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ram", "YOLLZ3272S", 1, 606992L },
                    { 12, 602310740678L, 943925554740L, 4419L, 1, new DateTime(1957, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shubham", "AJNZV5132D", 1, 507340L },
                    { 20, 643359853704L, 972016531683L, 40949L, 9, new DateTime(1991, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sunita", "OKOKS6038K", 1, 871959L },
                    { 9, 789953042547L, 265723038859L, 5890L, 10, new DateTime(1994, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kunal", "QKSXL5968C", 0, 520820L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankDetail_BankId",
                table: "BankDetail",
                column: "BankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankDetail");

            migrationBuilder.DropTable(
                name: "BankDetails");
        }
    }
}
