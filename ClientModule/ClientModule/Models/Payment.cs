using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("Payment")]
    public class Payment
    {
        [Key]
        public string Id { get; set; }
        [Required]
        bool inAdvance { get; set; }
        [Required]
        public PaymentMethodClass PaymentMethod { get; set; }

        public bool Complete() 
        {
            throw new NotImplementedException();
        }
    }
}
