﻿// <auto-generated />
using System;
using HotelReservations.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelReservations.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    [Migration("20240503081857_InitialCreate2")]
    partial class InitialCreate2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelReservations.Model.Guest", b =>
                {
                    b.Property<int>("guest_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("guest_id"));

                    b.Property<bool>("guest_is_active")
                        .HasColumnType("bit");

                    b.Property<string>("guest_jmbg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("guest_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("guest_reservation_id")
                        .HasColumnType("int");

                    b.Property<string>("guest_surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("reservation_id")
                        .HasColumnType("int");

                    b.HasKey("guest_id");

                    b.HasIndex("reservation_id");

                    b.ToTable("guest");
                });

            modelBuilder.Entity("HotelReservations.Model.Price", b =>
                {
                    b.Property<int>("price_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("price_id"));

                    b.Property<bool>("price_is_active")
                        .HasColumnType("bit");

                    b.Property<int>("price_reservation_type")
                        .HasColumnType("int");

                    b.Property<double>("price_value")
                        .HasColumnType("float");

                    b.Property<int?>("room_type_id1")
                        .HasColumnType("int");

                    b.HasKey("price_id");

                    b.HasIndex("room_type_id1");

                    b.ToTable("price");
                });

            modelBuilder.Entity("HotelReservations.Model.Reservation", b =>
                {
                    b.Property<int>("reservation_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("reservation_id"));

                    b.Property<DateTime>("end_date_time")
                        .HasColumnType("datetime2");

                    b.Property<bool>("reservation_is_active")
                        .HasColumnType("bit");

                    b.Property<bool>("reservation_is_finished")
                        .HasColumnType("bit");

                    b.Property<string>("reservation_room_number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("reservation_type")
                        .HasColumnType("int");

                    b.Property<DateTime>("start_date_time")
                        .HasColumnType("datetime2");

                    b.Property<double>("total_price")
                        .HasColumnType("float");

                    b.HasKey("reservation_id");

                    b.ToTable("reservation");
                });

            modelBuilder.Entity("HotelReservations.Model.Room", b =>
                {
                    b.Property<int>("room_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("room_id"));

                    b.Property<int?>("RoomTyperoom_type_id")
                        .HasColumnType("int");

                    b.Property<bool>("has_TV")
                        .HasColumnType("bit");

                    b.Property<bool>("has_mini_bar")
                        .HasColumnType("bit");

                    b.Property<bool>("room_is_active")
                        .HasColumnType("bit");

                    b.Property<string>("room_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("room_type_id")
                        .HasColumnType("int");

                    b.HasKey("room_id");

                    b.HasIndex("RoomTyperoom_type_id");

                    b.ToTable("room");
                });

            modelBuilder.Entity("HotelReservations.Model.RoomType", b =>
                {
                    b.Property<int>("room_type_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("room_type_id"));

                    b.Property<bool>("room_type_is_active")
                        .HasColumnType("bit");

                    b.Property<string>("room_type_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("room_type_id");

                    b.ToTable("room_type");
                });

            modelBuilder.Entity("HotelReservations.Model.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"));

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("user_is_active")
                        .HasColumnType("bit");

                    b.Property<string>("user_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("HotelReservations.Model.Guest", b =>
                {
                    b.HasOne("HotelReservations.Model.Reservation", "Reservation")
                        .WithMany("Guests")
                        .HasForeignKey("reservation_id");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("HotelReservations.Model.Price", b =>
                {
                    b.HasOne("HotelReservations.Model.RoomType", "room_type_id")
                        .WithMany()
                        .HasForeignKey("room_type_id1");

                    b.Navigation("room_type_id");
                });

            modelBuilder.Entity("HotelReservations.Model.Room", b =>
                {
                    b.HasOne("HotelReservations.Model.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTyperoom_type_id");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("HotelReservations.Model.Reservation", b =>
                {
                    b.Navigation("Guests");
                });
#pragma warning restore 612, 618
        }
    }
}
