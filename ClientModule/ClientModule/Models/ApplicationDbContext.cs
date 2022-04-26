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
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintState> ComplaintStates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatusClass> OrderStatuses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethodClass> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

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

