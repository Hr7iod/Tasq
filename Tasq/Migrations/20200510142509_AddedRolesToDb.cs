using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasq.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a57ecb81-89dc-4c49-bbf1-33953dadf067", "fe11a26e-3fb3-42be-9d2b-0a0c3f287a11", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "58aa7ad8-d6f2-4874-ae02-a8a7a26dd048", "81557243-7869-4287-b219-0d3126084006", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58aa7ad8-d6f2-4874-ae02-a8a7a26dd048");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a57ecb81-89dc-4c49-bbf1-33953dadf067");
        }
    }
}
