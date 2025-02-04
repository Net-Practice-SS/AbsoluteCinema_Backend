using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbsoluteCinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUslessRequiredsInHall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Halls_PlaceCount",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Halls_RowCount",
                table: "Halls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Halls_PlaceCount",
                table: "Halls",
                column: "PlaceCount",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Halls_RowCount",
                table: "Halls",
                column: "RowCount",
                unique: true);
        }
    }
}
