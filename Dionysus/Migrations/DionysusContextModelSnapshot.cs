﻿// <auto-generated />
using System;
using Dionysus.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dionysus.Migrations
{
    [DbContext(typeof(DionysusContext))]
    partial class DionysusContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dionysus.DBModels.Batch", b =>
                {
                    b.Property<int>("BatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("batch_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BarrelCount")
                        .HasColumnType("int")
                        .HasColumnName("barrel_count");

                    b.Property<DateTime>("FinishedStorage")
                        .HasColumnType("datetime")
                        .HasColumnName("finished_storage");

                    b.Property<DateTime>("StoredOn")
                        .HasColumnType("datetime")
                        .HasColumnName("stored_on");

                    b.Property<double>("TargetHumidity")
                        .HasColumnType("float")
                        .HasColumnName("target_humidity");

                    b.Property<double>("TargetTemperature")
                        .HasColumnType("float")
                        .HasColumnName("target_temperature");

                    b.HasKey("BatchId");

                    b.ToTable("batch");
                });

            modelBuilder.Entity("Dionysus.DBModels.ElevationCode", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("code");

                    b.HasKey("Code")
                        .HasName("PK__elevatio__357D4CF87BB60622");

                    b.ToTable("elevation_code");
                });

            modelBuilder.Entity("Dionysus.DBModels.EnvironmentalController", b =>
                {
                    b.Property<int>("ControllerPinNumber")
                        .HasColumnType("int")
                        .HasColumnName("controllerPinNumber");

                    b.Property<int?>("BatchId")
                        .HasColumnType("int")
                        .HasColumnName("batch_id");

                    b.Property<bool>("Mode")
                        .HasColumnType("bit")
                        .HasColumnName("mode");

                    b.Property<bool>("State")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.HasKey("ControllerPinNumber")
                        .HasName("PK__environm__60134B892EDAB51A");

                    b.HasIndex("BatchId");

                    b.ToTable("environmental_Controllers");
                });

            modelBuilder.Entity("Dionysus.DBModels.EnvironmentalReading", b =>
                {
                    b.Property<int>("ReadingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("reading_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BatchId")
                        .HasColumnType("int")
                        .HasColumnName("batch_id");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("date_time");

                    b.Property<double>("HumidityReading")
                        .HasColumnType("float")
                        .HasColumnName("humidity_reading");

                    b.Property<int>("SensorPinNumber")
                        .HasColumnType("int")
                        .HasColumnName("sensorPinNumber");

                    b.Property<double>("TemperatureReading")
                        .HasColumnType("float")
                        .HasColumnName("temperature_reading");

                    b.HasKey("ReadingId")
                        .HasName("PK__environm__8091F95A9A177427");

                    b.HasIndex("BatchId");

                    b.HasIndex("SensorPinNumber");

                    b.ToTable("environmental_readings");
                });

            modelBuilder.Entity("Dionysus.DBModels.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("rating_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("AcidityRating")
                        .HasColumnType("tinyint")
                        .HasColumnName("acidity_rating");

                    b.Property<int>("BatchId")
                        .HasColumnType("int")
                        .HasColumnName("batch_id");

                    b.Property<byte>("BitternessRating")
                        .HasColumnType("tinyint")
                        .HasColumnName("bitterness_rating");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("feedback");

                    b.Property<byte>("OverallRating")
                        .HasColumnType("tinyint")
                        .HasColumnName("overall_rating");

                    b.Property<DateTime>("RatedOn")
                        .HasColumnType("datetime")
                        .HasColumnName("rated_on");

                    b.Property<byte>("SweatnessRating")
                        .HasColumnType("tinyint")
                        .HasColumnName("sweatness_rating");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.HasKey("RatingId");

                    b.HasIndex("BatchId");

                    b.HasIndex("Username");

                    b.ToTable("rating");
                });

            modelBuilder.Entity("Dionysus.DBModels.Sensor", b =>
                {
                    b.Property<int>("SensorPinNumber")
                        .HasColumnType("int")
                        .HasColumnName("sensorPinNumber");

                    b.Property<bool>("State")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.HasKey("SensorPinNumber")
                        .HasName("PK__sensor__22905D0D5367A191");

                    b.ToTable("sensor");
                });

            modelBuilder.Entity("Dionysus.DBModels.User", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("role");

                    b.HasKey("Username")
                        .HasName("PK___user__F3DBC573A7E3E165");

                    b.ToTable("_user");
                });

            modelBuilder.Entity("Dionysus.DBModels.EnvironmentalController", b =>
                {
                    b.HasOne("Dionysus.DBModels.Batch", "Batch")
                        .WithMany("EnvironmentalControllers")
                        .HasForeignKey("BatchId")
                        .HasConstraintName("FK__environme__batch__286302EC");

                    b.Navigation("Batch");
                });

            modelBuilder.Entity("Dionysus.DBModels.EnvironmentalReading", b =>
                {
                    b.HasOne("Dionysus.DBModels.Batch", "Batch")
                        .WithMany("EnvironmentalReadings")
                        .HasForeignKey("BatchId")
                        .HasConstraintName("FK__environme__batch__32E0915F")
                        .IsRequired();

                    b.HasOne("Dionysus.DBModels.Sensor", "SensorPinNumberNavigation")
                        .WithMany("EnvironmentalReadings")
                        .HasForeignKey("SensorPinNumber")
                        .HasConstraintName("FK__environme__senso__33D4B598")
                        .IsRequired();

                    b.Navigation("Batch");

                    b.Navigation("SensorPinNumberNavigation");
                });

            modelBuilder.Entity("Dionysus.DBModels.Rating", b =>
                {
                    b.HasOne("Dionysus.DBModels.Batch", "Batch")
                        .WithMany("Ratings")
                        .HasForeignKey("BatchId")
                        .HasConstraintName("FK__rating__batch_id__2F10007B")
                        .IsRequired();

                    b.HasOne("Dionysus.DBModels.User", "UsernameNavigation")
                        .WithMany("Ratings")
                        .HasForeignKey("Username")
                        .HasConstraintName("FK__rating__username__300424B4")
                        .IsRequired();

                    b.Navigation("Batch");

                    b.Navigation("UsernameNavigation");
                });

            modelBuilder.Entity("Dionysus.DBModels.Batch", b =>
                {
                    b.Navigation("EnvironmentalControllers");

                    b.Navigation("EnvironmentalReadings");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Dionysus.DBModels.Sensor", b =>
                {
                    b.Navigation("EnvironmentalReadings");
                });

            modelBuilder.Entity("Dionysus.DBModels.User", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
