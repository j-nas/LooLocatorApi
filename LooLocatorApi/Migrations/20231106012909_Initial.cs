using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace LooLocatorApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Bathrooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationName = table.Column<string>(type: "text", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "text", nullable: true),
                    Coordinates = table.Column<Point>(type: "geography (point)", nullable: false),
                    LocationType = table.Column<int>(type: "integer", nullable: false),
                    IsAccessible = table.Column<bool>(type: "boolean", nullable: false),
                    IsUnisex = table.Column<bool>(type: "boolean", nullable: false),
                    IsChangingTable = table.Column<bool>(type: "boolean", nullable: false),
                    IsFamilyFriendly = table.Column<bool>(type: "boolean", nullable: false),
                    IsPurchaseRequired = table.Column<bool>(type: "boolean", nullable: false),
                    IsKeyRequired = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bathrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CleanlinessRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BathroomId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleanlinessRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleanlinessRatings_Bathrooms_BathroomId",
                        column: x => x.BathroomId,
                        principalTable: "Bathrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CleanlinessRatings_BathroomId",
                table: "CleanlinessRatings",
                column: "BathroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CleanlinessRatings");

            migrationBuilder.DropTable(
                name: "Bathrooms");
        }
    }
}
