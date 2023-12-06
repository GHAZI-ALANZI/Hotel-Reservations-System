using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "reservation_type",
                table: "reservation",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "reservation_type",
                table: "reservation",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
