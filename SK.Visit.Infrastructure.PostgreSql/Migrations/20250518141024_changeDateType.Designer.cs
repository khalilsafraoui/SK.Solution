﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SK.Visit.Infrastructure.PostgreSql.Persistence;

#nullable disable

namespace SK.Visit.Infrastructure.PostgreSql.Migrations
{
    [DbContext(typeof(VisitDbContext))]
    [Migration("20250518141024_changeDateType")]
    partial class changeDateType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SK.Visit.Domain.Entities.Destination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasAnnotation("ErrorMessage", "Address is missing..");

                    b.Property<string>("AddressId")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasAnnotation("ErrorMessage", "AddressId is missing..");

                    b.Property<string>("AgentId")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasAnnotation("ErrorMessage", "AgentId is missing..");

                    b.Property<string>("CityId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasAnnotation("ErrorMessage", "CustomerId is missing..");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp")
                        .HasAnnotation("ErrorMessage", "Date is missing..");

                    b.Property<bool>("IsDelevery")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasAnnotation("ErrorMessage", "Name is missing..");

                    b.Property<string>("OrderId")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StateId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Destinations");
                });
#pragma warning restore 612, 618
        }
    }
}
