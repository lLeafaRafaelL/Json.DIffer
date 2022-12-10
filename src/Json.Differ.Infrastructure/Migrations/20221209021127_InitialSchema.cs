using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Json.Differ.Infrastructure.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comparison",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Result = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SequentialKey = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comparison", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "FileToCompare",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Side = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SequentialKey = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileToCompare", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonFileDiff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Field = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    FileSide = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ComparisonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonFileDiff", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ComparisonFileDiff_Comparison_ComparisonId",
                        column: x => x.ComparisonId,
                        principalTable: "Comparison",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComparisonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonFile", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ComparisonFile_Comparison_ComparisonId",
                        column: x => x.ComparisonId,
                        principalTable: "Comparison",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComparisonFile_FileToCompare_FileId",
                        column: x => x.FileId,
                        principalTable: "FileToCompare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comparison_SequentialKey",
                table: "Comparison",
                column: "SequentialKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonFile_ComparisonId",
                table: "ComparisonFile",
                column: "ComparisonId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonFile_FileId",
                table: "ComparisonFile",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonFileDiff_ComparisonId",
                table: "ComparisonFileDiff",
                column: "ComparisonId");

            migrationBuilder.CreateIndex(
                name: "IX_FileToCompare_SequentialKey",
                table: "FileToCompare",
                column: "SequentialKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComparisonFile");

            migrationBuilder.DropTable(
                name: "ComparisonFileDiff");

            migrationBuilder.DropTable(
                name: "FileToCompare");

            migrationBuilder.DropTable(
                name: "Comparison");
        }
    }
}
