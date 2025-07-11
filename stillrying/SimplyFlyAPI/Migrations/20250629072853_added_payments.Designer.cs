﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimplyFlyAPI.Data;

#nullable disable

namespace SimplyFlyAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250629072853_added_payments")]
    partial class added_payments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SimplyFlyAPI.Models.Airline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AirlineCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AirlineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Cancellation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CancelledAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRefunded")
                        .HasColumnType("bit");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Cancellations");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AirlineId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlightRouteId")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("FlightRouteId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.FlightRoute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("BaggageCheckInKg")
                        .HasColumnType("int");

                    b.Property<int>("CabinBagKg")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Fare")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FlightRoutes");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookingId")
                        .HasColumnType("int");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("bit");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("FlightId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Airline", b =>
                {
                    b.HasOne("SimplyFlyAPI.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Booking", b =>
                {
                    b.HasOne("SimplyFlyAPI.Models.Flight", "Flight")
                        .WithMany("Bookings")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimplyFlyAPI.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Cancellation", b =>
                {
                    b.HasOne("SimplyFlyAPI.Models.Booking", "Booking")
                        .WithOne("Cancellation")
                        .HasForeignKey("SimplyFlyAPI.Models.Cancellation", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Flight", b =>
                {
                    b.HasOne("SimplyFlyAPI.Models.Airline", "Airline")
                        .WithMany("Flights")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimplyFlyAPI.Models.FlightRoute", "FlightRoute")
                        .WithMany("Flights")
                        .HasForeignKey("FlightRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airline");

                    b.Navigation("FlightRoute");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Payment", b =>
                {
                    b.HasOne("SimplyFlyAPI.Models.Booking", "Booking")
                        .WithOne("Payment")
                        .HasForeignKey("SimplyFlyAPI.Models.Payment", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Seat", b =>
                {
                    b.HasOne("SimplyFlyAPI.Models.Booking", "Booking")
                        .WithMany("Seats")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SimplyFlyAPI.Models.Flight", "Flight")
                        .WithMany("Seats")
                        .HasForeignKey("FlightId");

                    b.Navigation("Booking");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Airline", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Booking", b =>
                {
                    b.Navigation("Cancellation");

                    b.Navigation("Payment");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.Flight", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.FlightRoute", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("SimplyFlyAPI.Models.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
