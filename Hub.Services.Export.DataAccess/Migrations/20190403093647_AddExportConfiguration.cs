using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Hub.Services.Export.DataAccess.Migrations
{
    public partial class AddExportConfiguration : Migration
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
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    ExportProgram = table.Column<string>(maxLength: 128, nullable: false),
                    ExportType = table.Column<string>(maxLength: 20, nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    PostExecute = table.Column<string>(maxLength: 255, nullable: true),
                    PreExecute = table.Column<string>(maxLength: 255, nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportConfiguration", x => x.ExportName);
                    table.UniqueConstraint("AK_ExportConfiguration_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExportGroup",
                schema: "exp",
                columns: table => new
                {
                    ExportName = table.Column<string>(maxLength: 128, nullable: false),
                    GroupName = table.Column<string>(maxLength: 128, nullable: false),
                    ArchiveFolder = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    ExternalFolder = table.Column<string>(maxLength: 250, nullable: false),
                    FileSuffix = table.Column<string>(maxLength: 50, nullable: true),
                    Filter = table.Column<string>(maxLength: 255, nullable: true),
                    GroupType = table.Column<string>(maxLength: 50, nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    InternalFolder = table.Column<string>(maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsIncremental = table.Column<bool>(type: "bit", nullable: true),
                    OrderBy = table.Column<string>(maxLength: 250, nullable: true),
                    Project = table.Column<string>(maxLength: 50, nullable: true),
                    QueueTable = table.Column<string>(maxLength: 128, nullable: true),
                    Role = table.Column<string>(maxLength: 50, nullable: true),
                    SendEmail = table.Column<bool>(type: "bit", nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    ToAddr = table.Column<string>(maxLength: 250, nullable: true),
                    UpdateExportURL = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportGroup", x => new { x.ExportName, x.GroupName });
                    table.UniqueConstraint("AK_ExportGroup_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportGroup_ExportConfiguration_ExportName",
                        column: x => x.ExportName,
                        principalSchema: "exp",
                        principalTable: "ExportConfiguration",
                        principalColumn: "ExportName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportObject",
                schema: "exp",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    ExcludeFields = table.Column<string>(maxLength: 250, nullable: true),
                    ExportName = table.Column<string>(maxLength: 128, nullable: false),
                    Filter = table.Column<string>(maxLength: 255, nullable: true),
                    GroupName = table.Column<string>(maxLength: 128, nullable: false),
                    ObjectName = table.Column<string>(maxLength: 128, nullable: true),
                    OrderBy = table.Column<string>(maxLength: 250, nullable: true),
                    OutputName = table.Column<string>(maxLength: 128, nullable: false),
                    PrimaryKey = table.Column<string>(maxLength: 250, nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    Source = table.Column<string>(maxLength: 255, nullable: true),
                    SourceType = table.Column<string>(maxLength: 10, nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportObject_ExportGroup_ExportName_GroupName",
                        columns: x => new { x.ExportName, x.GroupName },
                        principalSchema: "exp",
                        principalTable: "ExportGroup",
                        principalColumns: new[] { "ExportName", "GroupName" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportProperties",
                schema: "exp",
                columns: table => new
                {
                    ExportName = table.Column<string>(maxLength: 128, nullable: false),
                    GroupName = table.Column<string>(maxLength: 128, nullable: false),
                    PropertyName = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    PropertyValue = table.Column<string>(maxLength: 255, nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportProperties", x => new { x.ExportName, x.GroupName, x.PropertyName });
                    table.UniqueConstraint("AK_ExportProperties_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportProperties_ExportGroup_ExportName_GroupName",
                        columns: x => new { x.ExportName, x.GroupName },
                        principalSchema: "exp",
                        principalTable: "ExportGroup",
                        principalColumns: new[] { "ExportName", "GroupName" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportQueue",
                schema: "exp",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateExported = table.Column<DateTime>(nullable: true),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    ExportName = table.Column<string>(maxLength: 128, nullable: false),
                    GroupName = table.Column<string>(maxLength: 128, nullable: false),
                    ObjectName = table.Column<string>(maxLength: 255, nullable: false),
                    ReferenceId = table.Column<string>(maxLength: 255, nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportQueue_ExportGroup_ExportName_GroupName",
                        columns: x => new { x.ExportName, x.GroupName },
                        principalSchema: "exp",
                        principalTable: "ExportGroup",
                        principalColumns: new[] { "ExportName", "GroupName" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportObject_ExportName_GroupName",
                schema: "exp",
                table: "ExportObject",
                columns: new[] { "ExportName", "GroupName" });

            migrationBuilder.CreateIndex(
                name: "IX_ExportQueue_ExportName_GroupName",
                schema: "exp",
                table: "ExportQueue",
                columns: new[] { "ExportName", "GroupName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportObject",
                schema: "exp");

            migrationBuilder.DropTable(
                name: "ExportProperties",
                schema: "exp");

            migrationBuilder.DropTable(
                name: "ExportQueue",
                schema: "exp");

            migrationBuilder.DropTable(
                name: "ExportGroup",
                schema: "exp");

            migrationBuilder.DropTable(
                name: "ExportConfiguration",
                schema: "exp");
        }
    }
}
