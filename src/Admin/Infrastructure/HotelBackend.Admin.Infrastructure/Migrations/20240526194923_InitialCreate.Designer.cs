﻿// <auto-generated />
using System;
using HotelBackend.Admin.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelBackend.Admin.Infrastructure.Migrations
{
    [DbContext(typeof(AdminDataContext))]
    [Migration("20240526194923_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("admin")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HotelBackend.Admin.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("HotelId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Employees", "admin");
                });

            modelBuilder.Entity("HotelBackend.Admin.Domain.Entities.Hotel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Hotels", "admin");
                });

            modelBuilder.Entity("HotelBackend.Admin.Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Availability")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoomTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms", "admin");
                });

            modelBuilder.Entity("HotelBackend.Admin.Domain.Entities.RoomType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("RoomPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("RoomTypes", "admin");
                });

            modelBuilder.Entity("HotelBackend.Admin.Domain.Entities.Employee", b =>
                {
                    b.HasOne("HotelBackend.Admin.Domain.Entities.Hotel", null)
                        .WithMany("Employees")
                        .HasForeignKey("HotelId");
                });

            modelBuilder.Entity("HotelBackend.Admin.Domain.Entities.Room", b =>
                {
                    b.HasOne("HotelBackend.Admin.Domain.Entities.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelBackend.Admin.Domain.Entities.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("HotelBackend.Admin.Domain.Entities.RoomType", b =>
                {
                    b.HasOne("HotelBackend.Admin.Domain.Entities.Hotel", "Hotel")
                        .WithMany("RoomTypes")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HotelBackend.Admin.Domain.Entities.Hotel", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("RoomTypes");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelBackend.Admin.Domain.Entities.RoomType", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}