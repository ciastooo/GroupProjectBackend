using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupProjectBackend.Models.DB.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlaces_Places_PlaceId",
                table: "RoutePlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlaces_Routes_RouteId",
                table: "RoutePlaces");

            migrationBuilder.DropIndex(
                name: "IX_RoutePlaces_PlaceId",
                table: "RoutePlaces");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "RoutePlaces");

            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Routes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "RoutePlaces",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "RoutePlaces",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<double>(
                name: "AverageRating",
                table: "Places",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlaces_Routes_RouteId",
                table: "RoutePlaces",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlaces_Routes_RouteId",
                table: "RoutePlaces");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "RoutePlaces");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "RoutePlaces");

            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "RoutePlaces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "AverageRating",
                table: "Places",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.CreateIndex(
                name: "IX_RoutePlaces_PlaceId",
                table: "RoutePlaces",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlaces_Places_PlaceId",
                table: "RoutePlaces",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlaces_Routes_RouteId",
                table: "RoutePlaces",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
