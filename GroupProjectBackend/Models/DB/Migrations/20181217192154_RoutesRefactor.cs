using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupProjectBackend.Models.DB.Migrations
{
    public partial class RoutesRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlaces_Places_FromPlaceId",
                table: "RoutePlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlaces_Places_ToPlaceId",
                table: "RoutePlaces");

            migrationBuilder.DropIndex(
                name: "IX_RoutePlaces_FromPlaceId",
                table: "RoutePlaces");

            migrationBuilder.DropColumn(
                name: "FromPlaceId",
                table: "RoutePlaces");

            migrationBuilder.RenameColumn(
                name: "ToPlaceId",
                table: "RoutePlaces",
                newName: "PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePlaces_ToPlaceId",
                table: "RoutePlaces",
                newName: "IX_RoutePlaces_PlaceId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Routes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlaces_Places_PlaceId",
                table: "RoutePlaces",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutePlaces_Places_PlaceId",
                table: "RoutePlaces");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "PlaceId",
                table: "RoutePlaces",
                newName: "ToPlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_RoutePlaces_PlaceId",
                table: "RoutePlaces",
                newName: "IX_RoutePlaces_ToPlaceId");

            migrationBuilder.AddColumn<int>(
                name: "FromPlaceId",
                table: "RoutePlaces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RoutePlaces_FromPlaceId",
                table: "RoutePlaces",
                column: "FromPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlaces_Places_FromPlaceId",
                table: "RoutePlaces",
                column: "FromPlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePlaces_Places_ToPlaceId",
                table: "RoutePlaces",
                column: "ToPlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
