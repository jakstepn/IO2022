using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DeliveryModule.Models
{
    public class DeliveryModuleDbContext : DbContext
    {
        public DeliveryModuleDbContext(DbContextOptions<DeliveryModuleDbContext> options) : base(options)
        {

        }
    
        public DbSet<Client> Clients { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CourierStatusClass> CouriersStatusClasses { get; set; }
        public DbSet<OrderStatusClass> OrderStatusClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Here we make database use enum as key.
            modelBuilder.Entity<CourierStatusClass>()
                .Property(s => s.CourierStatus)
                .HasConversion<string>();

            //Here we create data for each payment method in the database
            modelBuilder.Entity<CourierStatusClass>()
                .HasData(new CourierStatusClass[]{
                    new CourierStatusClass{CourierStatus = CourierStatusClass.CourierStatusEnum.Available},
                    new CourierStatusClass{CourierStatus = CourierStatusClass.CourierStatusEnum.Busy},
                    new CourierStatusClass{CourierStatus = CourierStatusClass.CourierStatusEnum.AwayFromWork}
            });

            //Here we make database use enum as key.
            modelBuilder.Entity<OrderStatusClass>()
                .Property(s => s.OrderStatus)
                .HasConversion<string>();

            //Here we create data for each payment method in the database
            modelBuilder.Entity<OrderStatusClass>()
                .HasData(new OrderStatusClass[]{
                    new OrderStatusClass{OrderStatus = OrderStatusClass.OrderStatusEnum.ReadyToPickUp},
                    new OrderStatusClass{OrderStatus = OrderStatusClass.OrderStatusEnum.Pending},
                    new OrderStatusClass{OrderStatus = OrderStatusClass.OrderStatusEnum.Delivered},
                    new OrderStatusClass{OrderStatus = OrderStatusClass.OrderStatusEnum.InPreparation},
                    new OrderStatusClass{OrderStatus = OrderStatusClass.OrderStatusEnum.Rejected}
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
