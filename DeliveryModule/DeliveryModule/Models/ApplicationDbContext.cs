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
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           

            //base.OnModelCreating(modelBuilder);
        }
    }
}
