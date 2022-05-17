﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopModule.Data;

namespace ShopModule.Migrations
{
    [DbContext(typeof(ShopModuleDbContext))]
    [Migration("20220517190413_ForthDbMigration")]
    partial class ForthDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Complaints.Complaint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentStatus")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("ShopModule.Employees.ShopEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentState")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EmployedSince")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShopEmployees");
                });

            modelBuilder.Entity("ShopModule.Location.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ShopModule.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AddressFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClientAddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ConfirmedPayment")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientAddressId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ShopModule.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GrossPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<Guid>("OrderFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductFK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("OrderFK");

                    b.HasIndex("ProductFK");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ShopModule.Products.Product", b =>
                {
                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ProductTypeFK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TaxRate")
                        .HasColumnType("int");

                    b.HasKey("ProductName");

                    b.HasIndex("ProductTypeFK");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShopModule.Products.ProductType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("ShopModule.Employees.ShopManager", b =>
                {
                    b.HasBaseType("ShopModule.Employees.ShopEmployee");

                    b.ToTable("ShopManagers");
                });

            modelBuilder.Entity("ShopModule.Orders.Order", b =>
                {
                    b.HasOne("ShopModule.Location.Address", "ClientAddress")
                        .WithMany("Orders")
                        .HasForeignKey("ClientAddressId");

                    b.Navigation("ClientAddress");
                });

            modelBuilder.Entity("ShopModule.Orders.OrderItem", b =>
                {
                    b.HasOne("ShopModule.Orders.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopModule.Products.Product", "Product")
                        .WithMany("OrdersItems")
                        .HasForeignKey("ProductFK");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopModule.Products.Product", b =>
                {
                    b.HasOne("ShopModule.Products.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeFK");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("ShopModule.Employees.ShopManager", b =>
                {
                    b.HasOne("ShopModule.Employees.ShopEmployee", null)
                        .WithOne()
                        .HasForeignKey("ShopModule.Employees.ShopManager", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopModule.Location.Address", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ShopModule.Orders.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ShopModule.Products.Product", b =>
                {
                    b.Navigation("OrdersItems");
                });

            modelBuilder.Entity("ShopModule.Products.ProductType", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
