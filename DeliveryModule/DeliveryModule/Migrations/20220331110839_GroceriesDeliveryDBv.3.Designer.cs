﻿// <auto-generated />
using DeliveryModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeliveryModule.Migrations
{
    [DbContext(typeof(DeliveryModuleDbContext))]
    [Migration("20220331110839_GroceriesDeliveryDBv.3")]
    partial class GroceriesDeliveryDBv3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeliveryModule.Database_Models.CourierStatusClass", b =>
                {
                    b.Property<string>("CourierStatus")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CourierStatus");

                    b.ToTable("CourierStatusClass");

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
#pragma warning restore 612, 618
        }
    }
}
