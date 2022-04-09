using DeliveryModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DeliveryModule;
using Microsoft.AspNetCore.Identity;

namespace DeliveryModule.Models
{
    
    public class Courier
    {
        [Key]
        public Guid Id { get; set; }
        public Order CurrentOrder { get; set; }
        public string PhoneNumber { get; set; }        
        public CourierStatusEnum CurrentState { get; set; }
        public enum CourierStatusEnum
        {
            Available = 0,
            Busy = 1,
            Away = 2
        }
        public void SendMessage(string Message)
        {

        }
    }
    
}
