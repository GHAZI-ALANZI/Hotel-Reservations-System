using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    reservation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reservation_room_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reservation_type = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    start_date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_price = table.Column<double>(type: "float", nullable: false),
                    reservation_is_active = table.Column<bool>(type: "bit", nullable: false),
                    reservation_is_finished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation", x => x.reservation_id);
                });

            migrationBuilder.CreateTable(
                name: "room_type",
                columns: table => new
                {
                    room_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    room_type_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    room_type_is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_type", x => x.room_type_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_type = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    user_is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "guest",
                columns: table => new
                {
                    guest_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guest_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    guest_surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    guest_jmbg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    guest_is_active = table.Column<bool>(type: "bit", nullable: false),
                    guest_reservation_id = table.Column<int>(type: "int", nullable: false),
                    reservation_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guest", x => x.guest_id);
                    table.ForeignKey(
                        name: "FK_guest_reservation_reservation_id",
                        column: x => x.reservation_id,
                        principalTable: "reservation",
                        principalColumn: "reservation_id");
                });

            migrationBuilder.CreateTable(
                name: "price",
                columns: table => new
                {
                    price_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    room_type_id1 = table.Column<int>(type: "int", nullable: true),
                    price_reservation_type = table.Column<int>(type: "int", nullable: false),
                    price_value = table.Column<double>(type: "float", nullable: false),
                    price_is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_price", x => x.price_id);
                    table.ForeignKey(
                        name: "FK_price_room_type_room_type_id1",
                        column: x => x.room_type_id1,
                        principalTable: "room_type",
                        principalColumn: "room_type_id");
                });

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    room_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    room_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    has_TV = table.Column<bool>(type: "bit", nullable: false),
                    has_mini_bar = table.Column<bool>(type: "bit", nullable: false),
                    room_is_active = table.Column<bool>(type: "bit", nullable: false),
                    room_type_id = table.Column<int>(type: "int", nullable: false),
                    RoomTyperoom_type_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.room_id);
                    table.ForeignKey(
                        name: "FK_room_room_type_RoomTyperoom_type_id",
                        column: x => x.RoomTyperoom_type_id,
                        principalTable: "room_type",
                        principalColumn: "room_type_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_guest_reservation_id",
                table: "guest",
                column: "reservation_id");

            migrationBuilder.CreateIndex(
                name: "IX_price_room_type_id1",
                table: "price",
                column: "room_type_id1");

            migrationBuilder.CreateIndex(
                name: "IX_room_RoomTyperoom_type_id",
                table: "room",
                column: "RoomTyperoom_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guest");

            migrationBuilder.DropTable(
                name: "price");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "reservation");

            migrationBuilder.DropTable(
                name: "room_type");
        }
    }
}
