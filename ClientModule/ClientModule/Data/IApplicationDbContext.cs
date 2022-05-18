using Microsoft.EntityFrameworkCore;
using ClientModule.Database_Models;

namespace ClientModule.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Client> Clients { get; }
        public DbSet<Address> Addresses { get; }
        public DbSet<Complaint> Complaints { get; }
        public DbSet<ComplaintState> ComplaintStates { get; }
        public DbSet<Order> Orders { get; }
        public DbSet<OrderStatusClass> OrderStatuses { get; }
        public DbSet<Payment> Payments { get; }
        public DbSet<PaymentMethodClass> PaymentMethods { get; }
        public DbSet<Product> Products { get; }
        public DbSet<ShoppingCart> ShoppingCarts { get; }

        int SaveChanges();
    }
}
