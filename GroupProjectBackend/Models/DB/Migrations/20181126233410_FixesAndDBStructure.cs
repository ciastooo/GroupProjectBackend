using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupProjectBackend.Models.DB.Migrations
{
    public partial class FixesAndDBStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Places_PlaceId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Routes_RouteStartId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Ratings_UserRatingId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Ratings_RatingModelDtoId",
                table: "Routes");

            migrationBuilder.DropTable(
                name: "UserLoginModelDto");

            migrationBuilder.DropIndex(
                name: "IX_Routes_RatingModelDtoId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Places_RouteStartId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_UserRatingId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Categories_PlaceId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "RatingModelDtoId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Lattitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "RouteStartId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "UserRatingId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Longtitude",
                table: "Places",
                newName: "CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Routes",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Routes",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Ratings",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Ratings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Ratings",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullAddress",
                table: "Places",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Places",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Places",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Places",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "RoutePlaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RouteId = table.Column<int>(nullable: false),
                    FromPlaceId = table.Column<int>(nullable: false),
                    ToPlaceId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutePlaces_Places_FromPlaceId",
                        column: x => x.FromPlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoutePlaces_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutePlaces_Places_ToPlaceId",
                        column: x => x.ToPlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_PlaceId",
                table: "Ratings",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RouteId",
                table: "Ratings",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId1",
                table: "Ratings",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CategoryId",
                table: "Places",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutePlaces_FromPlaceId",
                table: "RoutePlaces",
                column: "FromPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutePlaces_RouteId",
                table: "RoutePlaces",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutePlaces_ToPlaceId",
                table: "RoutePlaces",
                column: "ToPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Categories_CategoryId",
                table: "Places",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Places_PlaceId",
                table: "Ratings",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Routes_RouteId",
                table: "Ratings",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId1",
                table: "Ratings",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Categories_CategoryId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Places_PlaceId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Routes_RouteId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId1",
                table: "Ratings");

            migrationBuilder.DropTable(
                name: "RoutePlaces");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_PlaceId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RouteId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserId1",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Places_CategoryId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Places",
                newName: "Longtitude");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Routes",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Routes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Length",
                table: "Routes",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "RatingModelDtoId",
                table: "Routes",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Comment",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullAddress",
                table: "Places",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "Lattitude",
                table: "Places",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RouteStartId",
                table: "Places",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserRatingId",
                table: "Places",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Description",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserLoginModelDto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(nullable: false),
                    UserRatingId = table.Column<int>(nullable: true),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginModelDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLoginModelDto_Ratings_UserRatingId",
                        column: x => x.UserRatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_RatingModelDtoId",
                table: "Routes",
                column: "RatingModelDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_RouteStartId",
                table: "Places",
                column: "RouteStartId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_UserRatingId",
                table: "Places",
                column: "UserRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_PlaceId",
                table: "Categories",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginModelDto_UserRatingId",
                table: "UserLoginModelDto",
                column: "UserRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Places_PlaceId",
                table: "Categories",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Routes_RouteStartId",
                table: "Places",
                column: "RouteStartId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Ratings_UserRatingId",
                table: "Places",
                column: "UserRatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Ratings_RatingModelDtoId",
                table: "Routes",
                column: "RatingModelDtoId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
