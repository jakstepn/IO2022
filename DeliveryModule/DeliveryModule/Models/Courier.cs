using DeliveryModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
            AvaibleForDelivery = 0,
            DuringDelivery = 1,
            NotAvaible = 2
        }
        public void SendMessage(string Message)
        {

        }
        public Courier()
        {

        }
    }
    
}
