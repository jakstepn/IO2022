﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryModule
{
    public class DbClient
    {
        [Key]
        public int id { get; set; }
        public string PhoneNumber { get; set; }
    }
}
