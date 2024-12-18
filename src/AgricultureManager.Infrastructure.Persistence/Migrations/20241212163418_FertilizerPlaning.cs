using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgricultureManager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FertilizerPlaning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FertilizerPlaning",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HarvestUnitId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    FertilizerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Dosage = table.Column<float>(type: "float", nullable: false),
                    UnitId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Comment = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FertilizerPlaning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FertilizerPlaning_Fertilizer_FertilizerId",
                        column: x => x.FertilizerId,
                        principalTable: "Fertilizer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FertilizerPlaning_HarvestUnit_HarvestUnitId",
                        column: x => x.HarvestUnitId,
                        principalTable: "HarvestUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FertilizerPlaning_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FertilizerPlaningSpecification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HarvestUnitId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FertilizerDetailId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FertilizerPlaningSpecification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FertilizerPlaningSpecification_FertilizerDetail_FertilizerDe~",
                        column: x => x.FertilizerDetailId,
                        principalTable: "FertilizerDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FertilizerPlaningSpecification_HarvestUnit_HarvestUnitId",
                        column: x => x.HarvestUnitId,
                        principalTable: "HarvestUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerPlaning_FertilizerId",
                table: "FertilizerPlaning",
                column: "FertilizerId");

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerPlaning_HarvestUnitId",
                table: "FertilizerPlaning",
                column: "HarvestUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerPlaning_UnitId",
                table: "FertilizerPlaning",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerPlaningSpecification_FertilizerDetailId",
                table: "FertilizerPlaningSpecification",
                column: "FertilizerDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerPlaningSpecification_HarvestUnitId",
                table: "FertilizerPlaningSpecification",
                column: "HarvestUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FertilizerPlaning");

            migrationBuilder.DropTable(
                name: "FertilizerPlaningSpecification");
        }
    }
}
