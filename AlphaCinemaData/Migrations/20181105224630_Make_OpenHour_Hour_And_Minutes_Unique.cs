using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaCinemaData.Migrations
{
    public partial class Make_OpenHour_Hour_And_Minutes_Unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OpenHours_Hours",
                table: "OpenHours");

            migrationBuilder.DropIndex(
                name: "IX_OpenHours_Minutes",
                table: "OpenHours");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "30f149b5-fc0c-4e90-9a8a-3536b7ba07da", "0c36e591-7319-4ab2-af93-3fff70410f88" });

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f73432b-332b-402f-8952-7854ba9cf09c", "0efc1faf-95d7-47f9-8da7-e3da2e790429", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_OpenHours_Hours_Minutes",
                table: "OpenHours",
                columns: new[] { "Hours", "Minutes" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OpenHours_Hours_Minutes",
                table: "OpenHours");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7f73432b-332b-402f-8952-7854ba9cf09c", "0efc1faf-95d7-47f9-8da7-e3da2e790429" });

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30f149b5-fc0c-4e90-9a8a-3536b7ba07da", "0c36e591-7319-4ab2-af93-3fff70410f88", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_OpenHours_Hours",
                table: "OpenHours",
                column: "Hours",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenHours_Minutes",
                table: "OpenHours",
                column: "Minutes",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);
        }
    }
}
