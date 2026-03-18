using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StructuraFlow.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ValidationRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckDuplicateColumns = table.Column<bool>(type: "bit", nullable: false),
                    CheckDuplicateBeams = table.Column<bool>(type: "bit", nullable: false),
                    CheckDuplicateSlabs = table.Column<bool>(type: "bit", nullable: false),
                    CheckNegativeValues = table.Column<bool>(type: "bit", nullable: false),
                    CheckMissingFields = table.Column<bool>(type: "bit", nullable: false),
                    CheckBeamReferences = table.Column<bool>(type: "bit", nullable: false),
                    CheckBeamStartEndSame = table.Column<bool>(type: "bit", nullable: false),
                    MinColumnHeight = table.Column<double>(type: "float", nullable: false),
                    MinColumnWidth = table.Column<double>(type: "float", nullable: false),
                    MinBeamLength = table.Column<double>(type: "float", nullable: false),
                    MinSlabThickness = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationRules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValidationRules");
        }
    }
}
