using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LooLocatorApi.Migrations
{
    /// <inheritdoc />
    public partial class snake_case_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleanlinessRatings_Bathrooms_BathroomId",
                table: "CleanlinessRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bathrooms",
                table: "Bathrooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CleanlinessRatings",
                table: "CleanlinessRatings");

            migrationBuilder.RenameTable(
                name: "Bathrooms",
                newName: "bathrooms");

            migrationBuilder.RenameTable(
                name: "CleanlinessRatings",
                newName: "cleanliness_ratings");

            migrationBuilder.RenameColumn(
                name: "Coordinates",
                table: "bathrooms",
                newName: "coordinates");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "bathrooms",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LocationType",
                table: "bathrooms",
                newName: "location_type");

            migrationBuilder.RenameColumn(
                name: "LocationName",
                table: "bathrooms",
                newName: "location_name");

            migrationBuilder.RenameColumn(
                name: "IsUnisex",
                table: "bathrooms",
                newName: "is_unisex");

            migrationBuilder.RenameColumn(
                name: "IsPurchaseRequired",
                table: "bathrooms",
                newName: "is_purchase_required");

            migrationBuilder.RenameColumn(
                name: "IsKeyRequired",
                table: "bathrooms",
                newName: "is_key_required");

            migrationBuilder.RenameColumn(
                name: "IsFamilyFriendly",
                table: "bathrooms",
                newName: "is_family_friendly");

            migrationBuilder.RenameColumn(
                name: "IsChangingTable",
                table: "bathrooms",
                newName: "is_changing_table");

            migrationBuilder.RenameColumn(
                name: "IsAccessible",
                table: "bathrooms",
                newName: "is_accessible");

            migrationBuilder.RenameColumn(
                name: "AdditionalInfo",
                table: "bathrooms",
                newName: "additional_info");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "cleanliness_ratings",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "cleanliness_ratings",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cleanliness_ratings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "cleanliness_ratings",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "BathroomId",
                table: "cleanliness_ratings",
                newName: "bathroom_id");

            migrationBuilder.RenameIndex(
                name: "IX_CleanlinessRatings_BathroomId",
                table: "cleanliness_ratings",
                newName: "ix_cleanliness_ratings_bathroom_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_bathrooms",
                table: "bathrooms",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cleanliness_ratings",
                table: "cleanliness_ratings",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cleanliness_ratings_bathrooms_bathroom_id",
                table: "cleanliness_ratings",
                column: "bathroom_id",
                principalTable: "bathrooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cleanliness_ratings_bathrooms_bathroom_id",
                table: "cleanliness_ratings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_bathrooms",
                table: "bathrooms");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cleanliness_ratings",
                table: "cleanliness_ratings");

            migrationBuilder.RenameTable(
                name: "bathrooms",
                newName: "Bathrooms");

            migrationBuilder.RenameTable(
                name: "cleanliness_ratings",
                newName: "CleanlinessRatings");

            migrationBuilder.RenameColumn(
                name: "coordinates",
                table: "Bathrooms",
                newName: "Coordinates");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Bathrooms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "location_type",
                table: "Bathrooms",
                newName: "LocationType");

            migrationBuilder.RenameColumn(
                name: "location_name",
                table: "Bathrooms",
                newName: "LocationName");

            migrationBuilder.RenameColumn(
                name: "is_unisex",
                table: "Bathrooms",
                newName: "IsUnisex");

            migrationBuilder.RenameColumn(
                name: "is_purchase_required",
                table: "Bathrooms",
                newName: "IsPurchaseRequired");

            migrationBuilder.RenameColumn(
                name: "is_key_required",
                table: "Bathrooms",
                newName: "IsKeyRequired");

            migrationBuilder.RenameColumn(
                name: "is_family_friendly",
                table: "Bathrooms",
                newName: "IsFamilyFriendly");

            migrationBuilder.RenameColumn(
                name: "is_changing_table",
                table: "Bathrooms",
                newName: "IsChangingTable");

            migrationBuilder.RenameColumn(
                name: "is_accessible",
                table: "Bathrooms",
                newName: "IsAccessible");

            migrationBuilder.RenameColumn(
                name: "additional_info",
                table: "Bathrooms",
                newName: "AdditionalInfo");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "CleanlinessRatings",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "CleanlinessRatings",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CleanlinessRatings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "CleanlinessRatings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "bathroom_id",
                table: "CleanlinessRatings",
                newName: "BathroomId");

            migrationBuilder.RenameIndex(
                name: "ix_cleanliness_ratings_bathroom_id",
                table: "CleanlinessRatings",
                newName: "IX_CleanlinessRatings_BathroomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bathrooms",
                table: "Bathrooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CleanlinessRatings",
                table: "CleanlinessRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CleanlinessRatings_Bathrooms_BathroomId",
                table: "CleanlinessRatings",
                column: "BathroomId",
                principalTable: "Bathrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
