using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasq.Migrations
{
    public partial class NewFieldsForTasq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58aa7ad8-d6f2-4874-ae02-a8a7a26dd048");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a57ecb81-89dc-4c49-bbf1-33953dadf067");

            migrationBuilder.AddColumn<string>(
                name: "AppointedTo",
                table: "Tasqs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Tasqs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tasqs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createDate",
                table: "Tasqs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dueDate",
                table: "Tasqs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "814bc28c-4643-4b85-858b-2d68f95a7b3d", "39f99246-0518-4beb-9027-46b73ad7f451", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dbd544cd-6cf9-4c61-a969-2132dc588f06", "51cbbe72-a686-4470-b1d2-ab79ee9786c6", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "814bc28c-4643-4b85-858b-2d68f95a7b3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbd544cd-6cf9-4c61-a969-2132dc588f06");

            migrationBuilder.DropColumn(
                name: "AppointedTo",
                table: "Tasqs");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Tasqs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tasqs");

            migrationBuilder.DropColumn(
                name: "createDate",
                table: "Tasqs");

            migrationBuilder.DropColumn(
                name: "dueDate",
                table: "Tasqs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a57ecb81-89dc-4c49-bbf1-33953dadf067", "fe11a26e-3fb3-42be-9d2b-0a0c3f287a11", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "58aa7ad8-d6f2-4874-ae02-a8a7a26dd048", "81557243-7869-4287-b219-0d3126084006", "Administrator", "ADMINISTRATOR" });
        }
    }
}
