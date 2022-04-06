using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientModule.Database_Models
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }
        public DbSet<Client> Clients;
        public DbSet<Address> Addresses;
        public DbSet<Complaint> Complaints;
        public DbSet<ComplaintState> ComplaintStates;
        public DbSet<Order> Orders;
        public DbSet<OrderStatusClass> OrderStatuses;
        public DbSet<Payment> Payments;
        public DbSet<PaymentMethodClass> PaymentMethods;
        public DbSet<Product> Products;
        public DbSet<ShoppingCart> ShoppingCarts;

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

