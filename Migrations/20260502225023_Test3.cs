using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "Textbooks");

            migrationBuilder.DropColumn(
                name: "ResponseText",
                table: "Textbooks");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "Patents");

            migrationBuilder.DropColumn(
                name: "ResponseText",
                table: "Patents");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "Monographs");

            migrationBuilder.DropColumn(
                name: "ResponseText",
                table: "Monographs");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "Dissertations");

            migrationBuilder.DropColumn(
                name: "ResponseText",
                table: "Dissertations");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "ResponseText",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ResponseText",
                table: "Articles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "Textbooks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseText",
                table: "Textbooks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "Patents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseText",
                table: "Patents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "Monographs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseText",
                table: "Monographs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "Dissertations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseText",
                table: "Dissertations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "Conferences",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseText",
                table: "Conferences",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseText",
                table: "Articles",
                type: "text",
                nullable: true);
        }
    }
}
