﻿// <auto-generated />
using System;
using FlgValidations.Microservice.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlgValidation.Microservice.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220724165943_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FlgValidation.Microservice.Domain.Study", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified");

                    b.Property<string>("SopStudyId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sop_study_id");

                    b.Property<int>("State")
                        .HasColumnType("integer")
                        .HasColumnName("state");

                    b.HasKey("Id")
                        .HasName("pk_studies");

                    b.ToTable("studies", (string)null);
                });

            modelBuilder.Entity("FlgValidation.Microservice.Domain.StudySeries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified");

                    b.Property<string>("SeriesId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("series_id");

                    b.Property<Guid?>("StudyId")
                        .HasColumnType("uuid")
                        .HasColumnName("study_id");

                    b.HasKey("Id")
                        .HasName("pk_study_series");

                    b.HasIndex("StudyId")
                        .HasDatabaseName("ix_study_series_study_id");

                    b.ToTable("study_series", (string)null);
                });

            modelBuilder.Entity("FlgValidation.Microservice.Domain.StudySeriesInstance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified");

                    b.Property<string>("SopInstanceId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sop_instance_id");

                    b.Property<int?>("StudySeriesId")
                        .HasColumnType("integer")
                        .HasColumnName("study_series_id");

                    b.HasKey("Id")
                        .HasName("pk_study_series_instance");

                    b.HasIndex("StudySeriesId")
                        .HasDatabaseName("ix_study_series_instance_study_series_id");

                    b.ToTable("study_series_instance", (string)null);
                });

            modelBuilder.Entity("FlgValidation.Microservice.Domain.StudySeries", b =>
                {
                    b.HasOne("FlgValidation.Microservice.Domain.Study", null)
                        .WithMany("Series")
                        .HasForeignKey("StudyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_study_series_studies_study_id");

                    b.OwnsOne("FlgValidation.Microservice.Domain.Thickness", "Thickness", b1 =>
                        {
                            b1.Property<int>("StudySeriesId")
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("thickness_value");

                            b1.HasKey("StudySeriesId");

                            b1.ToTable("study_series");

                            b1.WithOwner()
                                .HasForeignKey("StudySeriesId")
                                .HasConstraintName("fk_study_series_study_series_id");
                        });

                    b.Navigation("Thickness")
                        .IsRequired();
                });

            modelBuilder.Entity("FlgValidation.Microservice.Domain.StudySeriesInstance", b =>
                {
                    b.HasOne("FlgValidation.Microservice.Domain.StudySeries", null)
                        .WithMany("StudySeriesInstances")
                        .HasForeignKey("StudySeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_study_series_instance_study_series_study_series_id");
                });

            modelBuilder.Entity("FlgValidation.Microservice.Domain.Study", b =>
                {
                    b.Navigation("Series");
                });

            modelBuilder.Entity("FlgValidation.Microservice.Domain.StudySeries", b =>
                {
                    b.Navigation("StudySeriesInstances");
                });
#pragma warning restore 612, 618
        }
    }
}
