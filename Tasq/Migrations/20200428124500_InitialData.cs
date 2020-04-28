using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasq.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tasqs",
                columns: new[] { "TasqId", "Description", "Name", "ParentId" },
                values: new object[] { new Guid("be8eecad-96a0-4eba-9ba2-14dd7a88f5d9"), "Проверка дескрипшена", "Первая тестовая таска", null });

            migrationBuilder.InsertData(
                table: "Tasqs",
                columns: new[] { "TasqId", "Description", "Name", "ParentId" },
                values: new object[] { new Guid("3b6b5db1-45ca-460c-9bca-725d2d3a6747"), null, "Вторая тестовая таска", null });

            migrationBuilder.InsertData(
                table: "Tasqs",
                columns: new[] { "TasqId", "Description", "Name", "ParentId" },
                values: new object[] { new Guid("e7df029a-ff87-4ff8-89ab-d22d8ac3ce29"), "Проверка третий раз дескрипшена", "Третья тестовая таска", null });

            migrationBuilder.InsertData(
                table: "Tasqs",
                columns: new[] { "TasqId", "Description", "Name", "ParentId" },
                values: new object[] { new Guid("d917bd64-22ee-4942-bd7c-dc5ef2132d23"), null, "Вторая тестовая ПОДтаска", new Guid("3b6b5db1-45ca-460c-9bca-725d2d3a6747") });

            migrationBuilder.InsertData(
                table: "Tasqs",
                columns: new[] { "TasqId", "Description", "Name", "ParentId" },
                values: new object[] { new Guid("1c21f4b6-b7e5-45d8-a3da-5fb19dcab145"), "Проверка подПОДдескрипшена", "Вторая тестовая ПОДПОДтаска", new Guid("d917bd64-22ee-4942-bd7c-dc5ef2132d23") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasqs",
                keyColumn: "TasqId",
                keyValue: new Guid("1c21f4b6-b7e5-45d8-a3da-5fb19dcab145"));

            migrationBuilder.DeleteData(
                table: "Tasqs",
                keyColumn: "TasqId",
                keyValue: new Guid("be8eecad-96a0-4eba-9ba2-14dd7a88f5d9"));

            migrationBuilder.DeleteData(
                table: "Tasqs",
                keyColumn: "TasqId",
                keyValue: new Guid("e7df029a-ff87-4ff8-89ab-d22d8ac3ce29"));

            migrationBuilder.DeleteData(
                table: "Tasqs",
                keyColumn: "TasqId",
                keyValue: new Guid("d917bd64-22ee-4942-bd7c-dc5ef2132d23"));

            migrationBuilder.DeleteData(
                table: "Tasqs",
                keyColumn: "TasqId",
                keyValue: new Guid("3b6b5db1-45ca-460c-9bca-725d2d3a6747"));
        }
    }
}
