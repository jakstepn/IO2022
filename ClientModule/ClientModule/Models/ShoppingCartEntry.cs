using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("ShoppingCartEntry")]
    [Index(nameof(ShoppingCartEntry.ShoppingCartId))]
    [Index(nameof(ShoppingCartEntry.DbProductId))]
    public class ShoppingCartEntry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int DbProductId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual Product Product { get; set; }
    }
}
