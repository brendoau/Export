using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Hub.Services.Export.DataAccess.Migrations
{
    public partial class AddExportConfigurationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "exp");

            migrationBuilder.CreateTable(
                name: "ExportConfiguration",
                schema: "exp",
                columns: table => new
                {
                    ExportName = table.Column<string>(maxLength: 128, nullable: false),
                    ExportProgram = table.Column<string>(maxLength: 128, nullable: true),
                    ExportType = table.Column<string>(maxLength: 20, nullable: true),
                    PostExecute = table.Column<string>(maxLength: 255, nullable: true),
                    PreExecute = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportConfiguration", x => x.ExportName);
                });

            migrationBuilder.CreateTable(
                name: "ExportGroup",
                schema: "exp",
                columns: table => new
                {
                    ExportName = table.Column<string>(maxLength: 128, nullable: false),
                    GroupName = table.Column<string>(maxLength: 128, nullable: false),
                    ArchiveFolder = table.Column<string>(maxLength: 250, nullable: true),
                    ExternalFolder = table.Column<string>(maxLength: 250, nullable: true),
                    FileSuffix = table.Column<string>(maxLength: 50, nullable: true),
                    Filter = table.Column<string>(maxLength: 255, nullable: true),
                    GroupType = table.Column<string>(maxLength: 50, nullable: true),
                    InternalFolder = table.Column<string>(maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsIncremental = table.Column<bool>(type: "bit", nullable: false),
                    OrderBy = table.Column<string>(maxLength: 250, nullable: true),
                    Project = table.Column<string>(maxLength: 50, nullable: true),
                    QueueTable = table.Column<string>(maxLength: 128, nullable: true),
                    Role = table.Column<string>(maxLength: 50, nullable: true),
                    SendEmail = table.Column<bool>(type: "bit", nullable: false),
                    ToAddr = table.Column<string>(maxLength: 250, nullable: true),
                    UpdateExportURL = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportGroup", x => new { x.ExportName, x.GroupName });
                });

            migrationBuilder.CreateTable(
                name: "ExportObject",
                schema: "exp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExcludeFields = table.Column<string>(maxLength: 250, nullable: true),
                    ExportName = table.Column<string>(maxLength: 128, nullable: true),
                    Filter = table.Column<string>(maxLength: 255, nullable: true),
                    GroupName = table.Column<string>(maxLength: 128, nullable: true),
                    ObjectName = table.Column<string>(maxLength: 128, nullable: true),
                    OrderBy = table.Column<string>(maxLength: 250, nullable: true),
                    OutputName = table.Column<string>(maxLength: 128, nullable: true),
                    PrimaryKey = table.Column<string>(maxLength: 250, nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    Source = table.Column<string>(maxLength: 255, nullable: true),
                    SourceType = table.Column<string>(maxLength: 10, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportObject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExportProperties",
                schema: "exp",
                columns: table => new
                {
                    ExportName = table.Column<string>(maxLength: 128, nullable: false),
                    GroupName = table.Column<string>(maxLength: 128, nullable: false),
                    PropertyName = table.Column<string>(maxLength: 128, nullable: false),
                    PropertyValue = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportProperties", x => new { x.ExportName, x.GroupName, x.PropertyName });
                });

            migrationBuilder.CreateTable(
                name: "ExportQueue",
                schema: "exp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateExported = table.Column<DateTime>(nullable: false),
                    ExportName = table.Column<string>(maxLength: 128, nullable: true),
                    GroupName = table.Column<string>(maxLength: 128, nullable: true),
                    ObjectName = table.Column<string>(maxLength: 255, nullable: true),
                    ReferenceId = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportQueue", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportConfiguration",
                schema: "exp");

            migrationBuilder.DropTable(
                name: "ExportGroup",
                schema: "exp");

            migrationBuilder.DropTable(
                name: "ExportObject",
                schema: "exp");

            migrationBuilder.DropTable(
                name: "ExportProperties",
                schema: "exp");

            migrationBuilder.DropTable(
                name: "ExportQueue",
                schema: "exp");
        }
    }
}
