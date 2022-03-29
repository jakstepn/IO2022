using DeliveryModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DeliveryModule;

namespace DeliveryModule.Database_Models
{
    public class Courier
    {
        [Key]
        public int Id { get; set; }
        public Order CurrentOrder { get; set; }
        public string PhoneNumber { get; set; }        
        public CourierStatusClass CurrentState { get; set; }
    }
    
}
