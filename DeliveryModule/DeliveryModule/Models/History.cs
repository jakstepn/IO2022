using System;
using System.ComponentModel.DataAnnotations;

namespace DeliveryModule.Models
{
    public class History
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CourierId { get; set; }
        public Guid OrderId { get; set; }

        public History()
        {
                
        }
        public History(Guid CourierId, Guid OrderId)
        {
            this.CourierId=CourierId;
            this.OrderId=OrderId;
        }
    }
}
