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
    public class ApplicationDbContext : ApiAuthorizationDbContext<DbClient>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }
        public DbSet<DbClient> Clients;
        public DbSet<DbAddress> Addresses;
        public DbSet<DbComplaint> Complaints;
        public DbSet<DbComplaintState> ComplaintStates;
        public DbSet<DbOrder> Orders;
        public DbSet<DbOrderStatus> OrderStatuses;
        public DbSet<DbPayment> Payments;
        public DbSet<DbPaymentMethod> PaymentMethods;
        public DbSet<DbProduct> Products;
        public DbSet<DbShoppingCart> ShoppingCarts;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Here we make database use enum as key.
            modelBuilder.Entity<DbPaymentMethod>()
                .Property(s => s.PaymentMethod)
                .HasConversion<string>();

            //Here we create data for each payment method in the database
            modelBuilder.Entity<DbPaymentMethod>()
                .HasData(new DbPaymentMethod[]{
                    new DbPaymentMethod{PaymentMethod = DbPaymentMethod.PaymentMethodEnum.Cash},
                    new DbPaymentMethod{PaymentMethod = DbPaymentMethod.PaymentMethodEnum.Card}
            });

            //Here we make database use enum as key.
            modelBuilder.Entity<DbOrderStatus>()
                .Property(s => s.OrderStatus)
                .HasConversion<string>();

            //Here we create data for each order status in the database
            modelBuilder.Entity<DbOrderStatus>()
                .HasData(new DbOrderStatus[]{
                    new DbOrderStatus{OrderStatus = DbOrderStatus.OrderStatusEnum.WaitingForPayment},
                    new DbOrderStatus{OrderStatus = DbOrderStatus.OrderStatusEnum.OnTheWay},
                    new DbOrderStatus{OrderStatus = DbOrderStatus.OrderStatusEnum.Delivered}
            });

            //Here we make database use enum as key.
            modelBuilder.Entity<DbComplaintState>()
                .Property(s => s.State)
                .HasConversion<string>();

            //Here we create data for each order status in the database
            modelBuilder.Entity<DbComplaintState>()
                .HasData(new DbComplaintState[]{
                    new DbComplaintState{State = DbComplaintState.DbComplaintStateEnum.WaitingForShopResponse},
                    new DbComplaintState{State = DbComplaintState.DbComplaintStateEnum.WaitingForClientResponse},
                    new DbComplaintState{State = DbComplaintState.DbComplaintStateEnum.Finished},
                    new DbComplaintState{State = DbComplaintState.DbComplaintStateEnum.Closed}
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}

