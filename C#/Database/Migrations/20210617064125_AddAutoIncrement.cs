using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddAutoIncrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Accounts_Account_Id",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Account_Id",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Account_Id",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Account_Id",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Account_Id",
                table: "Clients",
                column: "Account_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Accounts_Account_Id",
                table: "Clients",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
