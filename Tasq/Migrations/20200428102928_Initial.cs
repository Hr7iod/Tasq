using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasq.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasqs",
                columns: table => new
                {
                    TasqId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TasqId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasqs", x => x.TasqId);
                    table.ForeignKey(
                        name: "FK_Tasqs_Tasqs_TasqId1",
                        column: x => x.TasqId1,
                        principalTable: "Tasqs",
                        principalColumn: "TasqId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasqs_TasqId1",
                table: "Tasqs",
                column: "TasqId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasqs");
        }
    }
}
