﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryModule.Models
{
    public class Courier
    {
        public enum CourierState {sittingOnAss, working }
        public Order CurrentOrder { get; set; }
        public string PhoneNumber { get; set; }
        public CourierState CurrentState { get; set; }
        void SendMessage(Message message) { }
    }
    
}