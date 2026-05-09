using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "ResponseText",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ScientificPublications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ScientificPublications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ScientificPublications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "ScientificPublications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseText",
                table: "ScientificPublications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ScientificPublications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ScientificPublications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
