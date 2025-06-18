using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyApi.Migrations
{
    /// <inheritdoc />
    public partial class IsinConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Manually added check constraint to ensure ISIN starts with two letters
            // Use a more precise pattern for ISIN: two uppercase letters, followed by 10 alphanumeric characters
            migrationBuilder.AddCheckConstraint(
                name: "CK_Companies_Isin_Format",
                table: "Companies",
                sql: "Isin LIKE '[A-Z][A-Z][A-Z0-9][A-Z0-9][A-Z0-9][A-Z0-9][A-Z0-9][A-Z0-9][A-Z0-9][A-Z0-9][A-Z0-9][A-Z0-9]'"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
