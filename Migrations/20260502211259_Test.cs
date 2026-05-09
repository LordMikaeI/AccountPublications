using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kursach_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Employees_EmployeeId",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_ScientificPublications_Employees_EmployeeId",
                table: "ScientificPublications");

            migrationBuilder.DropForeignKey(
                name: "FK_ScientificPublications_PublicationEditions_PublicationEditi~",
                table: "ScientificPublications");

            migrationBuilder.DropIndex(
                name: "IX_ScientificPublications_EmployeeId",
                table: "ScientificPublications");

            migrationBuilder.DropIndex(
                name: "IX_ScientificPublications_PublicationEditionId",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "PublicationEditionId",
                table: "ScientificPublications");

            migrationBuilder.RenameColumn(
                name: "PublicationDate",
                table: "ScientificPublications",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Doi",
                table: "ScientificPublications",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ScientificPublications",
                newName: "RINC");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ScientificPublications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "ScientificPublications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Publications",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Publications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Publications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ResponseText = table.Column<string>(type: "text", nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PublicationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ResponseText = table.Column<string>(type: "text", nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PublicationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conferences_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dissertations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ResponseText = table.Column<string>(type: "text", nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PublicationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dissertations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dissertations_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monographs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ResponseText = table.Column<string>(type: "text", nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PublicationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monographs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monographs_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ResponseText = table.Column<string>(type: "text", nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PublicationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patents_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Textbooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ResponseText = table.Column<string>(type: "text", nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PublicationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Textbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Textbooks_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScientificPublications_PublicationId",
                table: "ScientificPublications",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PublicationId",
                table: "Articles",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_PublicationId",
                table: "Conferences",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Dissertations_PublicationId",
                table: "Dissertations",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Monographs_PublicationId",
                table: "Monographs",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_PublicationId",
                table: "Patents",
                column: "PublicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Textbooks_PublicationId",
                table: "Textbooks",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Employees_EmployeeId",
                table: "Publications",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScientificPublications_Publications_PublicationId",
                table: "ScientificPublications",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Employees_EmployeeId",
                table: "Publications");

            migrationBuilder.DropForeignKey(
                name: "FK_ScientificPublications_Publications_PublicationId",
                table: "ScientificPublications");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "Dissertations");

            migrationBuilder.DropTable(
                name: "Monographs");

            migrationBuilder.DropTable(
                name: "Patents");

            migrationBuilder.DropTable(
                name: "Textbooks");

            migrationBuilder.DropIndex(
                name: "IX_ScientificPublications_PublicationId",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "ResponseText",
                table: "ScientificPublications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Publications");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ScientificPublications",
                newName: "Doi");

            migrationBuilder.RenameColumn(
                name: "RINC",
                table: "ScientificPublications",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ScientificPublications",
                newName: "PublicationDate");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ScientificPublications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicationEditionId",
                table: "ScientificPublications",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Publications",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificPublications_EmployeeId",
                table: "ScientificPublications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificPublications_PublicationEditionId",
                table: "ScientificPublications",
                column: "PublicationEditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Employees_EmployeeId",
                table: "Publications",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScientificPublications_Employees_EmployeeId",
                table: "ScientificPublications",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScientificPublications_PublicationEditions_PublicationEditi~",
                table: "ScientificPublications",
                column: "PublicationEditionId",
                principalTable: "PublicationEditions",
                principalColumn: "Id");
        }
    }
}
