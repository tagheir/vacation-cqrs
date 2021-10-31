using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMG.Notification.Data.Migrations
{
    public partial class intial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplates_EmailPurposes_EmailPurposeId",
                table: "EmailTemplates");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplates_EmailPurposeId",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "EmailPurposeId",
                table: "EmailTemplates");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_PurposeId",
                table: "EmailTemplates",
                column: "PurposeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplates_EmailPurposes_PurposeId",
                table: "EmailTemplates",
                column: "PurposeId",
                principalTable: "EmailPurposes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplates_EmailPurposes_PurposeId",
                table: "EmailTemplates");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplates_PurposeId",
                table: "EmailTemplates");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailPurposeId",
                table: "EmailTemplates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_EmailPurposeId",
                table: "EmailTemplates",
                column: "EmailPurposeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplates_EmailPurposes_EmailPurposeId",
                table: "EmailTemplates",
                column: "EmailPurposeId",
                principalTable: "EmailPurposes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
