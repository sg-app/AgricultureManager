using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgricultureManager.Module.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class AddTanProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TanProcess",
                table: "AccountingAccount",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TanProcess",
                table: "AccountingAccount");
        }
    }
}
