using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgricultureManager.Module.Accounting.Migrations
{
    /// <inheritdoc />
    public partial class AddCostTypeToBookingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CostType",
                table: "AccountingBookingType",
                type: "nvarchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostType",
                table: "AccountingBookingType");
        }
    }
}
