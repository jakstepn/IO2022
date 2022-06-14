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
    [Migration("20220614175727_UpdateProduct")]
    partial class UpdateProduct
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

                    b.Property<string>("CurrentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Complaints");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ffffffff-aaaa-0000-0000-000000000000"),
                            CurrentStatus = "Pending",
                            OrderId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Text = "test_complaint"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("ffffffff-cccc-cccc-0000-000000000000"),
                            CurrentState = 0,
                            Email = "testmail",
                            EmployedSince = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "testowy",
                            Name = "tester",
                            PhoneNumber = "000-000-000"
                        });
                });

            modelBuilder.Entity("ShopModule.Location.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eeeeeeee-dddd-cccc-0000-000000000000"),
                            City = "test",
                            Street = "test",
                            ZipCode = "test"
                        },
                        new
                        {
                            Id = new Guid("eeeeeeee-dddd-ffff-0000-000000000000"),
                            City = "test2",
                            Street = "test2",
                            ZipCode = "test2"
                        });
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

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientAddressId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eeeeeeee-cccc-aaaa-0000-000000000000"),
                            AdditionalInfo = "additional",
                            AddressFK = new Guid("eeeeeeee-dddd-ffff-0000-000000000000"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeliveryDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ShopModule.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("OrderFK");

                    b.HasIndex("ProductFK");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ffffffff-aaaa-cccc-a000-000000000000"),
                            Currency = "USD",
                            OrderFK = new Guid("eeeeeeee-cccc-aaaa-0000-000000000000"),
                            ProductFK = new Guid("d2e2c69e-2fae-4ecf-bef9-944e4334d9d4"),
                            Quantity = 1m
                        });
                });

            modelBuilder.Entity("ShopModule.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductTypeFK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeFK");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d2e2c69e-2fae-4ecf-bef9-944e4334d9d4"),
                            Available = true,
                            Price = 1m,
                            ProductName = "testName",
                            ProductTypeFK = "testingCategory2",
                            Quantity = 1
                        },
                        new
                        {
                            Id = new Guid("bc1131d5-d2a5-43a7-9e62-c17a3683f936"),
                            Available = true,
                            Price = 3m,
                            ProductName = "testName2",
                            ProductTypeFK = "testingCategory2",
                            Quantity = 2
                        },
                        new
                        {
                            Id = new Guid("eb364b98-4099-4227-a07a-273b5fba9383"),
                            Available = false,
                            Price = 5m,
                            ProductName = "testName3",
                            ProductTypeFK = "testingCategory2",
                            Quantity = 5
                        },
                        new
                        {
                            Id = new Guid("01f2bd0b-eb14-4f3d-93e8-204217ba2a95"),
                            Available = true,
                            Price = 6m,
                            ProductName = "testName4",
                            ProductTypeFK = "testingCategory",
                            Quantity = 6
                        });
                });

            modelBuilder.Entity("ShopModule.Products.ProductType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("ProductTypes");

                    b.HasData(
                        new
                        {
                            Name = "testingCategory"
                        },
                        new
                        {
                            Name = "testingCategory2"
                        });
                });

            modelBuilder.Entity("ShopModule.Employees.ShopManager", b =>
                {
                    b.HasBaseType("ShopModule.Employees.ShopEmployee");

                    b.ToTable("ShopManagers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ffffffff-cccc-ffff-0000-000000000000"),
                            CurrentState = 0,
                            EmployedSince = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
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
                        .HasForeignKey("ProductFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
