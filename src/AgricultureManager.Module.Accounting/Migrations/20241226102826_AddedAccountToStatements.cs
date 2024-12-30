using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgricultureManager.Module.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccountToStatements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "AccountingStatementOfAccountDocument",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingStatementOfAccountDocument_AccountId",
                table: "AccountingStatementOfAccountDocument",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingStatementOfAccountDocument_AccountingAccount_Accou~",
                table: "AccountingStatementOfAccountDocument",
                column: "AccountId",
                principalTable: "AccountingAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingStatementOfAccountDocument_AccountingAccount_Accou~",
                table: "AccountingStatementOfAccountDocument");

            migrationBuilder.DropIndex(
                name: "IX_AccountingStatementOfAccountDocument_AccountId",
                table: "AccountingStatementOfAccountDocument");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AccountingStatementOfAccountDocument");
        }
    }
}
