using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiveDeep.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerDay = table.Column<float>(type: "real", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BCD_Sizes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thickness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiveSuit_Sizes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuitTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fins_Sizes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sizes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstStep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondStep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Octopus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tank_Sizes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ProductId",
                table: "Bookings",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
