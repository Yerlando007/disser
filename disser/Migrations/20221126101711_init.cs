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
                name: "GOST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    ChoisedRukovoditelID = table.Column<int>(type: "integer", nullable: true),
                    OnWork = table.Column<bool>(type: "boolean", nullable: false),
                    isFinished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GOST", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherGOST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GOSTName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherGOST", x => x.Id);
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
                name: "RukovoditelWantWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IspoltitelID = table.Column<string>(type: "text", nullable: true),
                    File = table.Column<string>(type: "text", nullable: true),
                    WorkPercentage = table.Column<double>(type: "double precision", nullable: true),
                    isFinishedTask = table.Column<bool>(type: "boolean", nullable: true),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    GOSTId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RukovoditelWantWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RukovoditelWantWork_GOST_GOSTId",
                        column: x => x.GOSTId,
                        principalTable: "GOST",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SimilarGOST",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedGOSTID = table.Column<int>(type: "integer", nullable: false),
                    OtherGOSTID = table.Column<int>(type: "integer", nullable: false),
                    GOSTId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarGOST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimilarGOST_GOST_GOSTId",
                        column: x => x.GOSTId,
                        principalTable: "GOST",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersGosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gostID = table.Column<int>(type: "integer", nullable: false),
                    gostName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersGosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersGosts_GOST_gostID",
                        column: x => x.gostID,
                        principalTable: "GOST",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GOSTKeyWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KeyWords = table.Column<string>(type: "text", nullable: false),
                    GOSTID = table.Column<int>(type: "integer", nullable: false),
                    OtherGOSTId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GOSTKeyWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GOSTKeyWords_OtherGOST_OtherGOSTId",
                        column: x => x.OtherGOSTId,
                        principalTable: "OtherGOST",
                        principalColumn: "Id");
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
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GOSTKeyWords_OtherGOSTId",
                table: "GOSTKeyWords",
                column: "OtherGOSTId");

            migrationBuilder.CreateIndex(
                name: "IX_RukovoditelWantWork_GOSTId",
                table: "RukovoditelWantWork",
                column: "GOSTId");

            migrationBuilder.CreateIndex(
                name: "IX_SimilarGOST_GOSTId",
                table: "SimilarGOST",
                column: "GOSTId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGosts_gostID",
                table: "UsersGosts",
                column: "gostID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "GOSTKeyWords");

            migrationBuilder.DropTable(
                name: "RukovoditelWantWork");

            migrationBuilder.DropTable(
                name: "SimilarGOST");

            migrationBuilder.DropTable(
                name: "UsersGosts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OtherGOST");

            migrationBuilder.DropTable(
                name: "GOST");
        }
    }
}
