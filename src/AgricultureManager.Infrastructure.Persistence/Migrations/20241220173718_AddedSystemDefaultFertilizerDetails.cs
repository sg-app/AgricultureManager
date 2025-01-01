using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgricultureManager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedSystemDefaultFertilizerDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SystemEntry",
                table: "FertilizerDetail",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "FertilizerDetail",
                columns: new[] { "Id", "Comment", "Name", "SystemEntry" },
                values: new object[,]
                {
                    { new Guid("04433ca8-714f-4007-bd93-672b2d10ff36"), "Stickstoff", "N", true },
                    { new Guid("0d69cc79-e5b4-4c84-afed-0f9397a611cb"), "Phosphor", "P", true },
                    { new Guid("1b5bb848-475d-4d77-bf53-d3d6ff09db46"), "Kali", "K", true },
                    { new Guid("8cfee622-ef2f-44ec-b6ca-92db0e8ee8fe"), "Schwefel", "S", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FertilizerDetail",
                keyColumn: "Id",
                keyValue: new Guid("04433ca8-714f-4007-bd93-672b2d10ff36"));

            migrationBuilder.DeleteData(
                table: "FertilizerDetail",
                keyColumn: "Id",
                keyValue: new Guid("0d69cc79-e5b4-4c84-afed-0f9397a611cb"));

            migrationBuilder.DeleteData(
                table: "FertilizerDetail",
                keyColumn: "Id",
                keyValue: new Guid("1b5bb848-475d-4d77-bf53-d3d6ff09db46"));

            migrationBuilder.DeleteData(
                table: "FertilizerDetail",
                keyColumn: "Id",
                keyValue: new Guid("8cfee622-ef2f-44ec-b6ca-92db0e8ee8fe"));

            migrationBuilder.DropColumn(
                name: "SystemEntry",
                table: "FertilizerDetail");
        }
    }
}
