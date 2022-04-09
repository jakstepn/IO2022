using System;
using System.ComponentModel.DataAnnotations;

namespace DeliveryModule.Models
{
    public class Message
    {
        [Key]
        public Guid id { get; set; }
        public string Content { get; set; }
    }
}
