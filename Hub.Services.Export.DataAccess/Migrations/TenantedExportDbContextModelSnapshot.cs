﻿// <auto-generated />
using Hub.Services.Export.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Hub.Services.Export.DataAccess.Migrations
{
    [DbContext(typeof(TenantedExportDbContext))]
    partial class TenantedExportDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("exp")
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportConfiguration", b =>
                {
                    b.Property<string>("ExportName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateUpdated");

                    b.Property<string>("ExportProgram")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("ExportType")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PostExecute")
                        .HasMaxLength(255);

                    b.Property<string>("PreExecute")
                        .HasMaxLength(255);

                    b.Property<Guid>("TenantId");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("ExportName");

                    b.HasAlternateKey("Id");

                    b.ToTable("ExportConfiguration");
                });

            modelBuilder.Entity("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportGroup", b =>
                {
                    b.Property<string>("ExportName")
                        .HasMaxLength(128);

                    b.Property<string>("GroupName")
                        .HasMaxLength(128);

                    b.Property<string>("ArchiveFolder")
                        .HasMaxLength(250);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateUpdated");

                    b.Property<string>("ExternalFolder")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("FileSuffix")
                        .HasMaxLength(50);

                    b.Property<string>("Filter")
                        .HasMaxLength(255);

                    b.Property<string>("GroupType")
                        .HasMaxLength(50);

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InternalFolder")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsIncremental")
                        .HasColumnType("bit");

                    b.Property<string>("OrderBy")
                        .HasMaxLength(250);

                    b.Property<string>("Project")
                        .HasMaxLength(50);

                    b.Property<string>("QueueTable")
                        .HasMaxLength(128);

                    b.Property<string>("Role")
                        .HasMaxLength(50);

                    b.Property<bool?>("SendEmail")
                        .HasColumnType("bit");

                    b.Property<Guid>("TenantId");

                    b.Property<string>("ToAddr")
                        .HasMaxLength(250);

                    b.Property<bool>("UpdateExportURL")
                        .HasColumnType("bit");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("ExportName", "GroupName");

                    b.HasAlternateKey("Id");

                    b.ToTable("ExportGroup");
                });

            modelBuilder.Entity("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateUpdated");

                    b.Property<string>("ExcludeFields")
                        .HasMaxLength(250);

                    b.Property<string>("ExportName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Filter")
                        .HasMaxLength(255);

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("ObjectName")
                        .HasMaxLength(128);

                    b.Property<string>("OrderBy")
                        .HasMaxLength(250);

                    b.Property<string>("OutputName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("PrimaryKey")
                        .HasMaxLength(250);

                    b.Property<int>("Sequence");

                    b.Property<string>("Source")
                        .HasMaxLength(255);

                    b.Property<string>("SourceType")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<Guid>("TenantId");

                    b.Property<string>("UpdatedBy");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ExportName", "GroupName");

                    b.ToTable("ExportObject");
                });

            modelBuilder.Entity("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportProperties", b =>
                {
                    b.Property<string>("ExportName")
                        .HasMaxLength(128);

                    b.Property<string>("GroupName")
                        .HasMaxLength(128);

                    b.Property<string>("PropertyName")
                        .HasMaxLength(128);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateUpdated");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PropertyValue")
                        .HasMaxLength(255);

                    b.Property<Guid>("TenantId");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("ExportName", "GroupName", "PropertyName");

                    b.HasAlternateKey("Id");

                    b.ToTable("ExportProperties");
                });

            modelBuilder.Entity("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportQueue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateExported");

                    b.Property<DateTime?>("DateUpdated");

                    b.Property<string>("ExportName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("ObjectName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ReferenceId")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<Guid>("TenantId");

                    b.Property<string>("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("ExportName", "GroupName");

                    b.ToTable("ExportQueue");
                });

            modelBuilder.Entity("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportGroup", b =>
                {
                    b.HasOne("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportConfiguration", "ExportConfiguration")
                        .WithMany("ExportGroups")
                        .HasForeignKey("ExportName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportObject", b =>
                {
                    b.HasOne("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportGroup", "ExportGroup")
                        .WithMany("ExportObjects")
                        .HasForeignKey("ExportName", "GroupName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportProperties", b =>
                {
                    b.HasOne("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportGroup", "ExportGroup")
                        .WithMany("ExportProperties")
                        .HasForeignKey("ExportName", "GroupName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportQueue", b =>
                {
                    b.HasOne("Hub.Services.Export.DataAccess.Model.Db.Tenanted.ExportGroup", "ExportGroup")
                        .WithMany("ExportQueues")
                        .HasForeignKey("ExportName", "GroupName")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
