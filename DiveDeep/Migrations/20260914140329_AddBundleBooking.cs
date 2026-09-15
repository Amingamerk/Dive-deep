using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiveDeep.Migrations
{
    /// <inheritdoc />
    public partial class AddBundleBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MAskSnorkels_Products_ProductId",
                table: "MAskSnorkels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MAskSnorkels",
                table: "MAskSnorkels");

            migrationBuilder.RenameTable(
                name: "MAskSnorkels",
                newName: "MaskSnorkels");

            migrationBuilder.AddColumn<int>(
                name: "BundleBookingId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaskSnorkels",
                table: "MaskSnorkels",
                column: "ProductId");

            migrationBuilder.CreateTable(
                name: "BundleBookings",
                columns: table => new
                {
                    BundleBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BundleId = table.Column<int>(type: "int", nullable: false),
                    BundleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercent = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleBookings", x => x.BundleBookingId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BundleBookingId",
                table: "Bookings",
                column: "BundleBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BundleBookings_BundleBookingId",
                table: "Bookings",
                column: "BundleBookingId",
                principalTable: "BundleBookings",
                principalColumn: "BundleBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaskSnorkels_Products_ProductId",
                table: "MaskSnorkels",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BundleBookings_BundleBookingId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_MaskSnorkels_Products_ProductId",
                table: "MaskSnorkels");

            migrationBuilder.DropTable(
                name: "BundleBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaskSnorkels",
                table: "MaskSnorkels");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BundleBookingId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BundleBookingId",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "MaskSnorkels",
                newName: "MAskSnorkels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MAskSnorkels",
                table: "MAskSnorkels",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_MAskSnorkels_Products_ProductId",
                table: "MAskSnorkels",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
