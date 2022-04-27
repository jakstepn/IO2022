﻿// <auto-generated />
using System;
using DeliveryModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeliveryModule.Migrations
{
    [DbContext(typeof(DeliveryModuleDbContext))]
    [Migration("20220406081332_deliverv8")]
    partial class deliverv8
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeliveryModule.Models.Client", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("DeliveryModule.Models.Courier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CurrentOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentState")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentOrderId");

                    b.ToTable("Couriers");
                });

            modelBuilder.Entity("DeliveryModule.Models.Message", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DeliveryModule.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Clientid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RequestedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Clientid");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DeliveryModule.Models.Courier", b =>
                {
                    b.HasOne("DeliveryModule.Models.Order", "CurrentOrder")
                        .WithMany()
                        .HasForeignKey("CurrentOrderId");

                    b.Navigation("CurrentOrder");
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
