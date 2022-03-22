using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryModule.Database_Models
{
    public class DbOrder
    {
        [Key]
        public int Id { get; set; }
        public DateTime RequestedTime { get; set; }
        public string MyProperty { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public DbClient Client { get; set; }
    }
}
