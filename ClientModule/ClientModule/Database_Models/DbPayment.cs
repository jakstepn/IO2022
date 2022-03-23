using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("Payment")]
    [Index(nameof(DbPayment.PaymentMethod))]
    public class DbPayment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        bool inAdvance { get; set; }
        [Required]
        public DbPaymentMethod PaymentMethod { get; set; }

        public bool IsPaymentComplete()
        {
            throw new NotImplementedException();
        }
    }
}
