using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaCinemaData.Migrations
{
    public partial class User_and_Movie_Image_and_CreatedOn_NotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8e2700f0-96ff-443c-89aa-a99e5205e418", "c7ba712b-9971-4c74-8c7c-6ddb8bac102f" });

            migrationBuilder.RenameColumn(
                name: "AvatarImageName",
                table: "AspNetUsers",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Movies",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d51a5aa9-a596-4e28-a000-d96813c5765e", "98a6dbac-1b1a-4eef-8c18-6ca07927abdc", "Admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d51a5aa9-a596-4e28-a000-d96813c5765e", "98a6dbac-1b1a-4eef-8c18-6ca07927abdc" });

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "AspNetUsers",
                newName: "AvatarImageName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8e2700f0-96ff-443c-89aa-a99e5205e418", "c7ba712b-9971-4c74-8c7c-6ddb8bac102f", "Admin", null });
        }
    }
}
