using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace disser.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreatedGOST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    ChoisedRukovoditelID = table.Column<int>(type: "integer", nullable: true),
                    OnWork = table.Column<bool>(type: "boolean", nullable: false),
                    WorkPercentage = table.Column<int>(type: "integer", nullable: false),
                    EndedFile = table.Column<string>(type: "text", nullable: true),
                    isFinished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatedGOST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FIO = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Mail = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    isVerify = table.Column<bool>(type: "boolean", nullable: false),
                    LeaderID = table.Column<int>(type: "integer", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllGOST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GOSTName = table.Column<string>(type: "text", nullable: false),
                    KeyWords = table.Column<string>(type: "text", nullable: false),
                    CreatedGOSTId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllGOST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllGOST_CreatedGOST_CreatedGOSTId",
                        column: x => x.CreatedGOSTId,
                        principalTable: "CreatedGOST",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RukovoditelWantWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    CommentFromIspolnitel = table.Column<string>(type: "text", nullable: true),
                    CommentToIspolnitel = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    File = table.Column<string>(type: "text", nullable: false),
                    IspolnitelFile = table.Column<string>(type: "text", nullable: true),
                    isFinishedTask = table.Column<bool>(type: "boolean", nullable: false),
                    RukovoditelId = table.Column<int>(type: "integer", nullable: false),
                    IspolnitelId = table.Column<int>(type: "integer", nullable: true),
                    CreatedGOSTId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RukovoditelWantWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RukovoditelWantWork_CreatedGOST_CreatedGOSTId",
                        column: x => x.CreatedGOSTId,
                        principalTable: "CreatedGOST",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimilarFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GOSTName = table.Column<string>(type: "text", nullable: false),
                    SimilarFiles = table.Column<string>(type: "text", nullable: false),
                    CreatedGOSTId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimilarFiles_CreatedGOST_CreatedGOSTId",
                        column: x => x.CreatedGOSTId,
                        principalTable: "CreatedGOST",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranslateFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkPercentage = table.Column<int>(type: "integer", nullable: false),
                    TranslateFileName = table.Column<string>(type: "text", nullable: false),
                    CommentFromTranslator = table.Column<string>(type: "text", nullable: false),
                    CommentToTranslator = table.Column<string>(type: "text", nullable: true),
                    TranslatorId = table.Column<int>(type: "integer", nullable: false),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedGOSTId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslateFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslateFile_CreatedGOST_CreatedGOSTId",
                        column: x => x.CreatedGOSTId,
                        principalTable: "CreatedGOST",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    DocumentName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllGOST_CreatedGOSTId",
                table: "AllGOST",
                column: "CreatedGOSTId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RukovoditelWantWork_CreatedGOSTId",
                table: "RukovoditelWantWork",
                column: "CreatedGOSTId");

            migrationBuilder.CreateIndex(
                name: "IX_SimilarFiles_CreatedGOSTId",
                table: "SimilarFiles",
                column: "CreatedGOSTId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslateFile_CreatedGOSTId",
                table: "TranslateFile",
                column: "CreatedGOSTId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllGOST");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "RukovoditelWantWork");

            migrationBuilder.DropTable(
                name: "SimilarFiles");

            migrationBuilder.DropTable(
                name: "TranslateFile");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CreatedGOST");
        }
    }
}
