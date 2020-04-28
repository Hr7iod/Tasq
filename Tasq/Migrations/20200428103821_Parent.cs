using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasq.Migrations
{
    public partial class Parent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasqs_Tasqs_TasqId1",
                table: "Tasqs");

            migrationBuilder.DropIndex(
                name: "IX_Tasqs_TasqId1",
                table: "Tasqs");

            migrationBuilder.DropColumn(
                name: "TasqId1",
                table: "Tasqs");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Tasqs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasqs_ParentId",
                table: "Tasqs",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasqs_Tasqs_ParentId",
                table: "Tasqs",
                column: "ParentId",
                principalTable: "Tasqs",
                principalColumn: "TasqId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasqs_Tasqs_ParentId",
                table: "Tasqs");

            migrationBuilder.DropIndex(
                name: "IX_Tasqs_ParentId",
                table: "Tasqs");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Tasqs");

            migrationBuilder.AddColumn<Guid>(
                name: "TasqId1",
                table: "Tasqs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasqs_TasqId1",
                table: "Tasqs",
                column: "TasqId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasqs_Tasqs_TasqId1",
                table: "Tasqs",
                column: "TasqId1",
                principalTable: "Tasqs",
                principalColumn: "TasqId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
