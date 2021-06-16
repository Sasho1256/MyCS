using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ClientandAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clients",
                newName: "GB_Flag");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Clients",
                newName: "Time_with_Bank");

            migrationBuilder.AddColumn<int>(
                name: "Account_Id",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Age_of_Applicant",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Application_Date",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Application_Month",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Application_Score",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Current_Delinquency_status",
                table: "Clients",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "Gross_Annual_Income",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Home_Telephone_Number",
                table: "Clients",
                type: "varchar(1)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Marital_Status",
                table: "Clients",
                type: "varchar(1)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Occupation_Code",
                table: "Clients",
                type: "varchar(1)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Residential_Status",
                table: "Clients",
                type: "varchar(1)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Time_at_Address",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Time_in_Employment",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Account_Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Account_Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Final_Decision = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cheque_Card_Flag = table.Column<string>(type: "varchar(1)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Existing_Customer_Flag = table.Column<string>(type: "varchar(1)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Insurance_Required = table.Column<string>(type: "varchar(1)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number_of_Dependants = table.Column<int>(type: "int", nullable: false),
                    Number_of_Payments = table.Column<int>(type: "int", nullable: false),
                    Promotion_Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weight_Factor = table.Column<double>(type: "double", nullable: false),
                    Bureau_Score = table.Column<int>(type: "int", nullable: false),
                    SP_ER_Reference = table.Column<int>(type: "int", nullable: false),
                    SP_Number_Of_Searches_L6M = table.Column<int>(type: "int", nullable: false),
                    SP_Number_of_CCJs = table.Column<int>(type: "int", nullable: false),
                    split = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Client_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Clients_Client_Id",
                        column: x => x.Client_Id,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Account_Id",
                table: "Clients",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Client_Id",
                table: "Accounts",
                column: "Client_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Accounts_Account_Id",
                table: "Clients",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Accounts_Account_Id",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Account_Id",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Account_Id",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Age_of_Applicant",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Application_Date",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Application_Month",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Application_Score",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Current_Delinquency_status",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Gross_Annual_Income",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Home_Telephone_Number",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Marital_Status",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Occupation_Code",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Residential_Status",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Time_at_Address",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Time_in_Employment",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Time_with_Bank",
                table: "Clients",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "GB_Flag",
                table: "Clients",
                newName: "Name");
        }
    }
}
