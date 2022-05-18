using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ClientModule.Database_Models;

namespace ClientModule.Data
{
    public class ApplicationDbContext: DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<ComplaintState> ComplaintStates { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatusClass> OrderStatuses { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMethodClass> PaymentMethods { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Here we make database use enum as key.
            modelBuilder.Entity<PaymentMethodClass>()
                .Property(s => s.PaymentMethod)
                .HasConversion<string>();

            //Here we create data for each payment method in the database
            modelBuilder.Entity<PaymentMethodClass>()
                .HasData(new PaymentMethodClass[]{
                    new PaymentMethodClass{PaymentMethod = PaymentMethodClass.PaymentMethodEnum.Cash},
                    new PaymentMethodClass{PaymentMethod = PaymentMethodClass.PaymentMethodEnum.Card}
            });

            //Here we make database use enum as key.
            modelBuilder.Entity<OrderStatusClass>()
                .Property(s => s.OrderStatus)
                .HasConversion<string>();

            //Here we create data for each order status in the database
            modelBuilder.Entity<OrderStatusClass>()
                .HasData(new OrderStatusClass[]{
                    new OrderStatusClass{OrderStatus = OrderStatusClass.OrderStatusEnum.WaitingForPayment},
                    new OrderStatusClass{OrderStatus = OrderStatusClass.OrderStatusEnum.OnTheWay},
                    new OrderStatusClass{OrderStatus = OrderStatusClass.OrderStatusEnum.Delivered}
            });

            //Here we make database use enum as key.
            modelBuilder.Entity<ComplaintState>()
                .Property(s => s.State)
                .HasConversion<string>();

            //Here we create data for each order status in the database
            modelBuilder.Entity<ComplaintState>()
                .HasData(new ComplaintState[]{
                    new ComplaintState{State = ComplaintState.DbComplaintStateEnum.WaitingForShopResponse},
                    new ComplaintState{State = ComplaintState.DbComplaintStateEnum.WaitingForClientResponse},
                    new ComplaintState{State = ComplaintState.DbComplaintStateEnum.Finished},
                    new ComplaintState{State = ComplaintState.DbComplaintStateEnum.Closed}
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}

