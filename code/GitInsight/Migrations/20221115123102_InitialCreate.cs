using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GitInsight.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Path = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LatestCommit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    DBRepositoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Repositories_DBRepositoryId",
                        column: x => x.DBRepositoryId,
                        principalTable: "Repositories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Commits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DBAuthorId = table.Column<int>(type: "INTEGER", nullable: true),
                    DBRepositoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commits_Authors_DBAuthorId",
                        column: x => x.DBAuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Commits_Repositories_DBRepositoryId",
                        column: x => x.DBRepositoryId,
                        principalTable: "Repositories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_DBRepositoryId",
                table: "Authors",
                column: "DBRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Commits_DBAuthorId",
                table: "Commits",
                column: "DBAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Commits_DBRepositoryId",
                table: "Commits",
                column: "DBRepositoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commits");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Repositories");
        }
    }
}
