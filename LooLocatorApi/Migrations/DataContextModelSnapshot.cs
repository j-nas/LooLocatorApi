﻿// <auto-generated />
using System;
using LooLocatorApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LooLocatorApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LooLocatorApi.Models.Bathroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("text");

                    b.Property<Point>("Coordinates")
                        .IsRequired()
                        .HasColumnType("geography (point)");

                    b.Property<bool>("IsAccessible")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsChangingTable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFamilyFriendly")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsKeyRequired")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPurchaseRequired")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUnisex")
                        .HasColumnType("boolean");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LocationType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Bathrooms");
                });

            modelBuilder.Entity("LooLocatorApi.Models.CleanlinessRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BathroomId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BathroomId");

                    b.ToTable("CleanlinessRatings");
                });

            modelBuilder.Entity("LooLocatorApi.Models.CleanlinessRating", b =>
                {
                    b.HasOne("LooLocatorApi.Models.Bathroom", "Bathroom")
                        .WithMany("CleanlinessRatings")
                        .HasForeignKey("BathroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bathroom");
                });

            modelBuilder.Entity("LooLocatorApi.Models.Bathroom", b =>
                {
                    b.Navigation("CleanlinessRatings");
                });
#pragma warning restore 612, 618
        }
    }
}
