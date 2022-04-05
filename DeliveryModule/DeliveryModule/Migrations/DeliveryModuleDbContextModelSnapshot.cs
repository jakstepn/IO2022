﻿// <auto-generated />
using System;
using DeliveryModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeliveryModule.Migrations
{
    [DbContext(typeof(DeliveryModuleDbContext))]
    partial class DeliveryModuleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeliveryModule.Models.Client", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("DeliveryModule.Models.Courier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurrentOrderId")
                        .HasColumnType("int");

                    b.Property<string>("CurrentStateCourierStatus")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentOrderId");

                    b.HasIndex("CurrentStateCourierStatus");

                    b.ToTable("Couriers");
                });

            modelBuilder.Entity("DeliveryModule.Models.CourierStatusClass", b =>
                {
                    b.Property<string>("CourierStatus")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CourierStatus");

                    b.ToTable("CouriersStatusClasses");

                    b.HasData(
                        new
                        {
                            CourierStatus = "Available"
                        },
                        new
                        {
                            CourierStatus = "Busy"
                        },
                        new
                        {
                            CourierStatus = "AwayFromWork"
                        });
                });

            modelBuilder.Entity("DeliveryModule.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Clientid")
                        .HasColumnType("int");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("MyProperty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RequestedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Clientid");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DeliveryModule.Models.OrderStatusClass", b =>
                {
                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderStatus");

                    b.ToTable("OrderStatusClasses");

                    b.HasData(
                        new
                        {
                            OrderStatus = "WaitingForPayment"
                        },
                        new
                        {
                            OrderStatus = "OnTheWay"
                        },
                        new
                        {
                            OrderStatus = "Delivered"
                        });
                });

            modelBuilder.Entity("DeliveryModule.Models.Courier", b =>
                {
                    b.HasOne("DeliveryModule.Models.Order", "CurrentOrder")
                        .WithMany()
                        .HasForeignKey("CurrentOrderId");

                    b.HasOne("DeliveryModule.Models.CourierStatusClass", "CurrentState")
                        .WithMany()
                        .HasForeignKey("CurrentStateCourierStatus");

                    b.Navigation("CurrentOrder");

                    b.Navigation("CurrentState");
                });

            modelBuilder.Entity("DeliveryModule.Models.Order", b =>
                {
                    b.HasOne("DeliveryModule.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("Clientid");

                    b.Navigation("Client");
                });
#pragma warning restore 612, 618
        }
    }
}
