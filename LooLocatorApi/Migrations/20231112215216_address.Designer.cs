﻿// <auto-generated />
using System;
using LooLocatorApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LooLocatorApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231112215216_address")]
    partial class address
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LooLocatorApi.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("BathroomId")
                        .HasColumnType("uuid")
                        .HasColumnName("bathroom_id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("line1");

                    b.Property<string>("Line2")
                        .HasColumnType("text")
                        .HasColumnName("line2");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("postal_code");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("province");

                    b.HasKey("Id")
                        .HasName("pk_addresses");

                    b.HasIndex("BathroomId")
                        .IsUnique()
                        .HasDatabaseName("ix_addresses_bathroom_id");

                    b.ToTable("addresses", (string)null);
                });

            modelBuilder.Entity("LooLocatorApi.Models.Bathroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AdditionalInfo")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("additional_info");

                    b.Property<Point>("Coordinates")
                        .IsRequired()
                        .HasColumnType("geography (point)")
                        .HasColumnName("coordinates");

                    b.Property<bool>("IsAccessible")
                        .HasColumnType("boolean")
                        .HasColumnName("is_accessible");

                    b.Property<bool>("IsChangingTable")
                        .HasColumnType("boolean")
                        .HasColumnName("is_changing_table");

                    b.Property<bool>("IsFamilyFriendly")
                        .HasColumnType("boolean")
                        .HasColumnName("is_family_friendly");

                    b.Property<bool>("IsKeyRequired")
                        .HasColumnType("boolean")
                        .HasColumnName("is_key_required");

                    b.Property<bool>("IsPurchaseRequired")
                        .HasColumnType("boolean")
                        .HasColumnName("is_purchase_required");

                    b.Property<bool>("IsUnisex")
                        .HasColumnType("boolean")
                        .HasColumnName("is_unisex");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("location_name");

                    b.Property<int>("LocationType")
                        .HasColumnType("integer")
                        .HasColumnName("location_type");

                    b.HasKey("Id")
                        .HasName("pk_bathrooms");

                    b.ToTable("bathrooms", (string)null);
                });

            modelBuilder.Entity("LooLocatorApi.Models.CleanlinessRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("BathroomId")
                        .HasColumnType("uuid")
                        .HasColumnName("bathroom_id");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("comment");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_cleanliness_ratings");

                    b.HasIndex("BathroomId")
                        .HasDatabaseName("ix_cleanliness_ratings_bathroom_id");

                    b.ToTable("cleanliness_ratings", (string)null);
                });

            modelBuilder.Entity("LooLocatorApi.Models.Address", b =>
                {
                    b.HasOne("LooLocatorApi.Models.Bathroom", "Bathroom")
                        .WithOne("Address")
                        .HasForeignKey("LooLocatorApi.Models.Address", "BathroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_addresses_bathrooms_bathroom_id");

                    b.Navigation("Bathroom");
                });

            modelBuilder.Entity("LooLocatorApi.Models.CleanlinessRating", b =>
                {
                    b.HasOne("LooLocatorApi.Models.Bathroom", "Bathroom")
                        .WithMany("CleanlinessRatings")
                        .HasForeignKey("BathroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cleanliness_ratings_bathrooms_bathroom_id");

                    b.Navigation("Bathroom");
                });

            modelBuilder.Entity("LooLocatorApi.Models.Bathroom", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("CleanlinessRatings");
                });
#pragma warning restore 612, 618
        }
    }
}
