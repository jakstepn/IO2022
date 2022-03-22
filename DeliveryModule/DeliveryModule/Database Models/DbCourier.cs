using DeliveryModule.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DeliveryModule;

namespace DeliveryModule.Database_Models
{
    public class DbCourier
    {
        [Key]
        public int Id { get; set; }
        public DbOrder CurrentOrder { get; set; }
        public string PhoneNumber { get; set; }        
        public Courier.CourierState CurrentState { get; set; }
    }
    
}
