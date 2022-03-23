using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("ShoppingCartEntry")]
    [Index(nameof(DbShoppingCartEntry.ShoppingCartId))]
    [Index(nameof(DbShoppingCartEntry.DbProductId))]
    public class DbShoppingCartEntry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int DbProductId { get; set; }

        public virtual DbShoppingCart ShoppingCart { get; set; }
        public virtual DbProduct Product { get; set; }
    }
}
