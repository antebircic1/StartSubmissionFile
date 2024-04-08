using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StartSubmissionFile.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
				name: "Submission",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false),
					BarcodeId = table.Column<int>(type: "int", nullable: false),
					FarmId = table.Column<int>(type: "int", nullable: false),
					DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
					IsGenerated = table.Column<bool>(type: "bit", nullable: false),
					DateGenerated = table.Column<DateTime>(type: "datetime2", nullable: true),
					Error = table.Column<string>(type: "nvarchar(max)", nullable: true),
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Submission", x => x.Id);
				});
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
				name: "Submission");
		}
    }
}
