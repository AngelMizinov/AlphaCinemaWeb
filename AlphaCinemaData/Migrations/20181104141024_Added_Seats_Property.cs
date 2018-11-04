using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaCinemaData.Migrations
{
    public partial class Added_Seats_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedMovies_AspNetUsers_UserId",
                table: "WatchedMovies");

            migrationBuilder.DropIndex(
                name: "IX_WatchedMovies_UserId_ProjectionId_Date",
                table: "WatchedMovies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_Name",
                table: "Movies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "91482aeb-bcec-4966-bfae-f70079278cfb", "ae70f826-f04c-4dfe-85f0-1c7a39aeed81" });

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WatchedMovies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Projections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Movies",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Movies",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30f149b5-fc0c-4e90-9a8a-3536b7ba07da", "0c36e591-7319-4ab2-af93-3fff70410f88", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_WatchedMovies_UserId_ProjectionId_Date",
                table: "WatchedMovies",
                columns: new[] { "UserId", "ProjectionId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Name",
                table: "Movies",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedMovies_AspNetUsers_UserId",
                table: "WatchedMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedMovies_AspNetUsers_UserId",
                table: "WatchedMovies");

            migrationBuilder.DropIndex(
                name: "IX_WatchedMovies_UserId_ProjectionId_Date",
                table: "WatchedMovies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_Name",
                table: "Movies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "30f149b5-fc0c-4e90-9a8a-3536b7ba07da", "0c36e591-7319-4ab2-af93-3fff70410f88" });

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Projections");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WatchedMovies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Movies",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Movies",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 400);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "91482aeb-bcec-4966-bfae-f70079278cfb", "ae70f826-f04c-4dfe-85f0-1c7a39aeed81", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_WatchedMovies_UserId_ProjectionId_Date",
                table: "WatchedMovies",
                columns: new[] { "UserId", "ProjectionId", "Date" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Name",
                table: "Movies",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedMovies_AspNetUsers_UserId",
                table: "WatchedMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
